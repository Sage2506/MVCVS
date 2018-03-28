using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace modelViewController.Controllers
{
    public class CatalogoController : Controller
    {
        IProductsRepository products;
        public CatalogoController(){
            products = new MemoryProductsRepo();
        }
        // GET: Catalogo
        public ActionResult Index()
        {
            var model = products.allProducts()
                        .Select( p => new ProductModel(){
                            Code = p.Code,
                            Category = p.Category,
                            Description = p.Description,
                            Price = p.Price
                        });
            return View(model);
        }

        // GET: Catalogo/Details/5
        public ActionResult Details(string id)
        {
            var model = products.productDetails(id);
            var detailed = new ProductModel(){
                Code = model.Code,
                Description = model.Description,
                Category = model.Category,
                Price = model.Price,
            };
            return View(detailed);
        }

        // GET: Catalogo/Create
        public ActionResult Create()
        {
            var model = new ProductCreateModel();
            return View();
        }

        // POST: Catalogo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateModel model)
        {
            if(ModelState.IsValid){

            
            try
            {
                // TODO: Add insert logic here
                products.createProduct(new ProductEntity(){
                    Code = model.Code,
                    Description = model.Description,
                    Price = model.Price,
                    Category = model.Category
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            }
            return View();
        }

        // GET: Catalogo/Edit/5
        public ActionResult Edit(string id)
        {
            var product = products.productDetails(id);
            return View(new ProductEditModel(){
                Description = product.Description,
                Price = product.Price,
                Category = product.Category
            });
        }

        // POST: Catalogo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductEditModel model)
        {
            if(ModelState.IsValid){

            
            try
            {
                // TODO: Add update logic here
                products.updateData(new ProductEntity(){
                    Description = model.Description,
                    Price = model.Price,
                    Category = model.Category
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            }
            return View();
        }

        // GET: Catalogo/Delete/5
        public ActionResult Delete(string id)
        {
             var product = products.productDetails(id);
            
            return View(new ProductDeleteModel(){
                Code = product.Code,
                Category = product.Category
            });
        }

        // POST: Catalogo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                products.eraseProductCode(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}