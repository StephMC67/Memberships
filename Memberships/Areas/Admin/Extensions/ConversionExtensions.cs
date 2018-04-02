using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

    }
}