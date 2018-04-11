using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using Memberships.Areas.Admin.Models;
using Memberships.Entities;
using Memberships.Models;


namespace Memberships.Areas.Admin.Extensions
{
    public static class ConversionExtensions   // static pour méthode d'extension
    {

        // fonction de conversion d'une collection Product vers ProductModel
        //   ProductModel = Product + liste des productLinkText et liste productTypes.
        public static async Task<IEnumerable<ProductModel>> Convert(
            this IEnumerable<Product> products,
            ApplicationDbContext db)
        {

            if (products.Count().Equals(0))
                return new List<ProductModel>();

            var texts = await db.ProductLinkTexts.ToListAsync();
            var types = await db.ProductTypes.ToListAsync();

            return from p in products
                   select new ProductModel
                   {
                       Id = p.Id,
                       Title = p.Title,
                       Description = p.Description,
                       ImageUrl = p.ImageUrl,
                       ProductLinkTextId = p.ProductLinkTextId,
                       ProductTypeId = p.ProductTypeId,
                       ProductLinkTexts = texts,    // stockage liste des product link texts
                       ProductTypes = types         // stockage des product types définis dans la base.

                   };

        }



        // fonction de conversion d'un objet Product vers ProductModel
        public static async Task<ProductModel> Convert(
            this Product product,
            ApplicationDbContext db)
        {

            var text = await db.ProductLinkTexts.FirstOrDefaultAsync(p => p.Id.Equals(product.ProductLinkTextId));
            var type = await db.ProductTypes.FirstOrDefaultAsync(p => p.Id.Equals(product.ProductTypeId));

            var model = new ProductModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                ProductLinkTextId = product.ProductLinkTextId,
                ProductTypeId = product.ProductTypeId,
                //ProductLinkTexts = new List<ProductLinkText>(),    // liste vide
                //ProductTypes = new List<ProductType>()         // liste vide
                ProductLinkTexts = new List<ProductLinkText>() { text },    // liste initialisée immédiatement
                ProductTypes = new List<ProductType>() { type }          // liste initialisée immédiatement
            };

            // ajout des textes dans les listes correspondantes si utilisation de liste vide
            //model.ProductLinkTexts.Add(text);
            //model.ProductTypes.Add(type);
            return model;

        }

        // fonction de conversion d'une collection Product vers ProductModel
        //   ProductModel = Product + liste des productLinkText et liste productTypes.
        public static async Task<IEnumerable<ProductItemModel>> Convert(
            this IQueryable<ProductItem> productItems,
            ApplicationDbContext db)
        {
            // Iqueryable car requete double sur la base dans la meme requete
            if (productItems.Count().Equals(0))
                return new List<ProductItemModel>();

            return await (from pi in productItems
                          select new ProductItemModel
                          {
                              ItemId = pi.ItemId,
                              ProductId = pi.ProductId,
                              ItemTitle = db.Items.FirstOrDefault(i => i.Id.Equals(pi.ItemId)).Title,
                              ProductTitle = db.Products.FirstOrDefault(p => p.Id.Equals(pi.ProductId)).Title
                          }).ToListAsync();

        }


        // fonction de conversion d'un objet ProductItem vers ProductItemModel
        public static async Task<ProductItemModel> Convert(
            this ProductItem productitem,
            ApplicationDbContext db,
            bool addListData = true)
        {

            var model = new ProductItemModel
            {
                ItemId = productitem.ItemId,
                ProductId = productitem.ProductId,

                Items = addListData ? await db.Items.ToListAsync() : null,
                Products = addListData ?  await db.Products.ToListAsync() : null,
                ItemTitle = (db.Items.FirstOrDefault(i => i.Id.Equals(productitem.ItemId))).Title,
                ProductTitle = (db.Products.FirstOrDefault(p => p.Id.Equals(productitem.ProductId))).Title,

            };

            return model;

        }


        public static async Task<bool> CanChange(this ProductItem productItem, ApplicationDbContext db)
        {
            var oldPI = await db.ProductItems.CountAsync(pi =>
                pi.ProductId.Equals(productItem.OldProductId) &&
                pi.ItemId.Equals(productItem.OldItemId));

            var newPI = await db.ProductItems.CountAsync(pi =>
                pi.ProductId.Equals(productItem.ProductId) &&
                pi.ItemId.Equals(productItem.ItemId));

            // ancienne valeur doit exister une fois et la nouvelle valeur ne doit pas exister
            // test fait sur le "comptage" des "paires" d'elements correspondant 
            return (oldPI.Equals(1)) && (newPI.Equals(0));

        }

        public static async Task Change(this ProductItem productItem, ApplicationDbContext db)
        {
            var oldProductItem = await db.ProductItems.FirstOrDefaultAsync(pi =>
               pi.ProductId.Equals(productItem.OldProductId) &&
               pi.ItemId.Equals(productItem.OldItemId));

            var newProductItem = await db.ProductItems.FirstOrDefaultAsync(pi =>
               pi.ProductId.Equals(productItem.ProductId) &&
               pi.ItemId.Equals(productItem.ItemId));

            if (oldProductItem != null && newProductItem == null)
            {
                var newProductItem2 = new ProductItem
                {
                    ProductId = productItem.ProductId,
                    ItemId = productItem.ItemId
                };

                // transaction : 
                // - suppression ancien element
                // - ajout du nouveau
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        db.ProductItems.Remove(oldProductItem);
                        db.ProductItems.Add(newProductItem2);
                        await db.SaveChangesAsync();
                        transaction.Complete();
                    }
                    catch
                    {
                        transaction.Dispose(); // arrêt de la transaction et rollback
                    }
                }


        }
    }
}
}