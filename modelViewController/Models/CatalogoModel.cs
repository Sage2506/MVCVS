using System;
using System.ComponentModel.DataAnnotations;

namespace modelViewController{
    public class ProductModel{
        public string Code{get; set;}

        public string Category{get; set;}

        public string Description {get; set;}

        public decimal Price{get; set;}

        public string imagen{get; set;}
    }

    public class ProductEditModel {
        

        public string Category{get; set;}

        public string Description {get; set;}

        public decimal Price{get; set;}

        public string imagen{get; set;}
    }
    public class ProductCreateModel {
        [Required]
        public string Code{get; set;}
        public string Category{get; set;}

        public string Description {get; set;}
        [Required]
        public decimal Price{get; set;}

        public string imagen{get; set;}
    }

    public class ProductDeleteModel {
        public string Code{get; set;}

        public string Category{get; set;}
    }
}