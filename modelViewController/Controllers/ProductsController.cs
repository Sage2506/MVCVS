using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using modelViewController.Models;

namespace modelViewController.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
         IProductsRepository products;
        public ProductsController(){
            products = new AzureProductsRepo();
        } 
        [HttpGet]
        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            var model = (await products.allProducts())
                        .Select( p => new ProductModel(){
                            Code = p.Code,
                            Category = p.Category,
                            Description = p.Description,
                            Price = p.Price
                        });

            return model;
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<IActionResult> GetById(string id)
        {
            var model = await products.productDetails(id);
            var detailed = new ProductModel(){
                Code = model.Code,
                Description = model.Description,
                Category = model.Category,
                Price = model.Price,
            };
            return new ObjectResult(model);
        }
    }
}