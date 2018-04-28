using System;
using System.Collections.Generic;
//using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace modelViewController{
    public class AzureProductsRepo : IProductsRepository
    {
        private string azureConStr;
        public AzureProductsRepo() {
            azureConStr = @"DefaultEndpointsProtocol=https;AccountName=s100ne2g9;AccountKey=L7oSx/X7xuAbLS0kH9ePHXynuczH9fGSD7Ca3sI9ls2Es7ulmb983I/IEggc6TYGQ6vnV9s8KlENjT7JhkNrhw==;EndpointSuffix=core.windows.net";
        }
        public async Task<List<ProductEntity>> allProducts()
        {
            // Retrieve a reference to the table.
            CloudTable table = TableAzure();

            TableQuery<azureProduct> query = new TableQuery<azureProduct>();

            var token = new TableContinuationToken();
            var list = new List<ProductEntity>();
            foreach(azureProduct entity in await table.ExecuteQuerySegmentedAsync(query,token)){
                 list.Add(new ProductEntity(){
                    Code = entity.Code,
                    Category = entity.Category,
                    Description = entity.Description
                 });
            }
            return list;
        }

        public async Task<List<ProductEntity>> allProductsByCategory(string category)
        {
            return new List<ProductEntity>();
        }

        public async Task<bool> createProduct(ProductEntity newProduct)
        {
        // Retrieve a reference to the table.
        CloudTable table = TableAzure();

        // Create the table if it doesn't exist.
        var creada = table.CreateIfNotExistsAsync().Result;

        var azEnt =  new azureProduct(newProduct.Code);
        azEnt.Category = newProduct.Category;
        azEnt.Price = newProduct.Price.ToString("C");
        azEnt.Description = newProduct.Description;
        azEnt.image = newProduct.image;
        TableOperation insertOperation = TableOperation.Insert(azEnt);

        // Execute the insert operation.
        var x = await table.ExecuteAsync(insertOperation);

        return true;
        }

        public Task<bool>  eraseProductCode(string code)
        {
            throw new NotImplementedException();
        }

        private CloudTable TableAzure(){
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureConStr);

        // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("catalogo");
            return table;
        }
        public async Task<ProductEntity> productDetails(string code)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<azureProduct>("Smith","Ben");

            TableResult retrievedResult = await TableAzure().ExecuteAsync(retrieveOperation);

            if(retrievedResult != null)
            {
                var az = retrievedResult.Result as azureProduct;
                return new ProductEntity(){
                    Code = az.Code,
                    Description = az.Description,
                    Price = decimal.Parse(az.Price),
                    Category = az.Category,
                    image = az.image
                };
            }
            else{
                Console.WriteLine("Error");
            }
            return null;
        }

        public Task<bool>  updateData(ProductEntity toUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> updateImage(ProductEntity product, string image)
        {
            //throw new NotImplementedException();
            return true;
        }
    }
    public class azureProduct : TableEntity{
        public azureProduct(){

        }
        public azureProduct(String code){
            this.PartitionKey = code.Substring(0,3);
            this.RowKey = code;
            this.Code = code;
        }
         public string Code{get; set;}

        public string Category{get; set;}

        public string Description {get; set;}

        public string Price{get; set;}

        public string image{get; set;}

    }
}