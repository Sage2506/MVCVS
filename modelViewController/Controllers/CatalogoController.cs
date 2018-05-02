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
            products = new AzureProductsRepo();
        }
        // GET: Catalogo
        public async Task <ActionResult> Index()
        {
            var model = (await products.allProducts())
                        .Select( p => new ProductModel(){
                            Code = p.Code,
                            Category = p.Category,
                            Description = p.Description,
                            Price = p.Price
                        });
            return View(model);
        }

        // GET: Catalogo/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var model =(await products.productDetails(id));
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
        public async Task<ActionResult> Create(ProductCreateModel model)
        {
            if(ModelState.IsValid){

            
            try
            {
                // TODO: Add insert logic here
                await products.createProduct(new ProductEntity(){
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
        public async Task<ActionResult> Edit(string id)
        {
            var product = await products.productDetails(id);
            return View(new ProductEditModel(){
                Code = product.Code,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category
            });
        }

        // POST: Catalogo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit(ProductEditModel model)
        {
            if(ModelState.IsValid){

            
            try
            {
                // TODO: Add update logic here
                await products.updateData(new ProductEntity(){
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

        // GET: Catalogo/Delete/5
        public async Task <ActionResult> Delete(string id)
        {
             var product = (await products.productDetails(id));
            
            return View(new ProductDeleteModel(){
                Code = product.Code,
                Category = product.Category
            });
        }

        // POST: Catalogo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                await products.eraseProductCode(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}