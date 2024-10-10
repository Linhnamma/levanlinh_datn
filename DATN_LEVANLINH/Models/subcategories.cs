using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class subcategories
    {
        public int subcategory_id { get; set; }
        public string subcategory_name { get; set; }
        public int category_id { get; set; }

        public subcategories()
        {
        }

        public subcategories(int subcategory_id, string subcategory_name, int category_id)
        {
            this.subcategory_id = subcategory_id;
            this.subcategory_name = subcategory_name;
            this.category_id = category_id;
        }
    }
}