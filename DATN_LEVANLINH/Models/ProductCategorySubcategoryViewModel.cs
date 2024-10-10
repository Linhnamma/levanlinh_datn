using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class ProductCategorySubcategoryViewModel
    {
        public List<Products> products { get; set; }
        public List<CategorySubcategoryViewModel> categorysubcategory { get; set; }
        public ProductCategorySubcategoryViewModel()
        {
        }
        public ProductCategorySubcategoryViewModel(List<Products> products, List<CategorySubcategoryViewModel> categorysubcategory)
        {
            this.products = products;
            this.categorysubcategory = categorysubcategory;
        }

        
    }
}