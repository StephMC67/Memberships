using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Memberships.Entities;
using Memberships.Models;
using Memberships.Areas.Admin.Extensions;
using Memberships.Areas.Admin.Models;

namespace Memberships.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Product
        public async Task<ActionResult> Index()
        {
            var products = await db.Products.ToListAsync();
         
            // convert est utilisé via une Task, il faut donc ajouter await.
            //var model = products.Convert(db);
            var model = await products.Convert(db);

            return View(model);   
        }

        // GET: Admin/Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            // passage par productmodel
            var model = await product.Convert(db);
            //return View(product);
            return View(model);
        }

        // GET: Admin/Product/Create
        //public ActionResult Create()   ==> comme on utilise await et ToListAsync, il faut que la méthode soit "Async Task <>"
        public async Task<ActionResult> Create()
        {
            var model = new ProductModel
            {
                ProductLinkTexts = await db.ProductLinkTexts.ToListAsync(),
                ProductTypes = await db.ProductTypes.ToListAsync()           
            };

            return View(model);
        }

        // POST: Admin/Product/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,ImageUrl,ProductLinkTextId,ProductTypeId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            // on va réutiliser la fonction de conversion d'extension.
            // cette fonction utilise en entrée un IEnumerable<Product> et retourne un IEnumerable<task> <ProductModule>

            // création du Ienumerable utilisé en entrée et ajout du "product" correspondant à la recherche
            var prod = new List<Product>();
            prod.Add(product);
            // conversion en ProductModel pour l'affichage dans la vue à partir du IEnumerable<Product>
            var ProductModel = await prod.Convert(db);
        
            //OnActionExecuted ne retourne que le premier de la liste (il n'y a en a de toute façon que 1 dans la liste)
            return View(ProductModel.First()); 
        }

        // POST: Admin/Product/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,ImageUrl,ProductLinkTextId,ProductTypeId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            // passage par productmodel
            var model = await product.Convert(db);
            //return View(product);
            return View(model);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
