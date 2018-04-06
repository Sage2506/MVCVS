using System;
using System.Collections.Generic;
//using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace modelViewController{
    public class AzureProductsRepo : IProductsRepository
    {
        private string azureConStr;
        public AzureProductsRepo() {
            azureConStr = @"DefaultEndpointsProtocol=https;AccountName=s100ne2g9;AccountKey=L7oSx/X7xuAbLS0kH9ePHXynuczH9fGSD7Ca3sI9ls2Es7ulmb983I/IEggc6TYGQ6vnV9s8KlENjT7JhkNrhw==;EndpointSuffix=core.windows.net";
        }
        public List<ProductEntity> allProducts()
        {
            // Retrieve a reference to the table.
            CloudTable table = TableAzure();

            TableQuery<azureProduct> query = new TableQuery<azureProduct>();

            var token = new TableContinuationToken();
            var list = new List<ProductEntity>();
            foreach(azureProduct entity in table.ExecuteQuerySegmentedAsync(query,token).Result){
                 list.Add(new ProductEntity(){
                    Code = entity.Code,
                    Category = entity.Category,
                    Description = entity.Description
                 });
            }
            return list;
        }

        public List<ProductEntity> allProductsByCategory(string category)
        {
            return new List<ProductEntity>();
        }

        public void createProduct(ProductEntity newProduct){
        

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
        var x = table.ExecuteAsync(insertOperation).Result;
        }

        public void eraseProductCode(string code)
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
        public ProductEntity productDetails(string code)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<azureProduct>("Smith","Ben");

            TableResult retrievedResult = TableAzure().ExecuteAsync(retrieveOperation);

            if(retrievedResult != null)
            {
                Console.WriteLine(((ProductEntity)retrievedResult.Result).Code);
            }
            else{
                Console.WriteLine("Error");
            }
        }

        public void updateData(ProductEntity toUpdate)
        {
            throw new NotImplementedException();
        }

        public void updateImage(ProductEntity product, string image)
        {
            throw new NotImplementedException();
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