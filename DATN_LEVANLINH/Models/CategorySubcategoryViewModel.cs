using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class CategorySubcategoryViewModel
    {
        public categories category { get; set; }
        public List<subcategories> subcategory { get; set; }

        public CategorySubcategoryViewModel()
        {
        }

        public CategorySubcategoryViewModel(categories category, List<subcategories> subcategory)
        {
            this.category = category;
            this.subcategory = subcategory;
        }
    }
}