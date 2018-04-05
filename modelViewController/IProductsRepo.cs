using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.Azure.Storage; // Namespace for StorageAccounts

namespace modelViewController{
    public interface IProductsRepository
    {
        
        List<ProductEntity> allProducts();

        List<ProductEntity> allProductsByCategory(string category);

        ProductEntity productDetails(string code);

        void createProduct(ProductEntity newProduct);

        void updateData(ProductEntity toUpdate);

        void updateImage(ProductEntity product, string image);
        
        void eraseProductCode(string code);
    }

    public class MemoryProductsRepo : IProductsRepository
    {
        private static List<ProductEntity> db;
        static MemoryProductsRepo(){
            db = new List<ProductEntity>();
            db.Add(new ProductEntity(){
                Code = "0001",
                Category = "Category 1",
                Description = "producto 1",
                Price = 100,
                image = string.Empty
            });
            db.Add(new ProductEntity(){
                Code = "0002",
                Category = "Category 3",
                Description = "producto 2",
                Price = 200,
                image = string.Empty
            });
            db.Add(new ProductEntity(){
                Code = "0003",
                Category = "Category 2",
                Description = "producto 3",
                Price = 300,
                image = string.Empty
            });
            db.Add(new ProductEntity(){
                Code = "0004",
                Category = "Category 1",
                Description = "producto 4",
                Price = 400,
                image = string.Empty
            });
            db.Add(new ProductEntity(){
                Code = "0005",
                Category = "Category 2",
                Description = "producto 5",
                Price = 500,
                image = string.Empty
            });
            db.Add(new ProductEntity(){
                Code = "0006",
                Category = "Category 3",
                Description = "producto 6",
                Price = 600,
                image = string.Empty
            });
        }
        public List<ProductEntity> allProducts()
        {
            return db.ToList();
        }

        public List<ProductEntity> allProductsByCategory(string category)
        {
            return db.Where(s => s.Code == category).ToList();
        }

        public void createProduct(ProductEntity newProduct)
        {
            db.Add(newProduct);
        }

        public void eraseProductCode(string code)
        {
            var exists = db.FirstOrDefault(p => p.Code == code);
            if (exists != null){
                db.Remove(exists);
            }
        }

        public ProductEntity productDetails(string code)
        {
            return db.FirstOrDefault(d => d.Code == code);
        }

        public void updateData(ProductEntity toUpdate)
        {
           var exists = db.FirstOrDefault(p => p.Code == toUpdate.Code);
            if (exists != null){
                exists.Category = toUpdate.Category;
                exists.Price = toUpdate.Price;
                exists.Description = toUpdate.Description;
            }
        }

        public void updateImage(ProductEntity product, string image)
        {
            throw new NotImplementedException();
        }
    }

    public class ProductEntity{
        
         public string Code{get; set;}

        public string Category{get; set;}
        public string Description {get; set;}

        public decimal Price{get; set;}

        public string image{get; set;}
    }
}