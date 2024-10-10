using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class categories
    {
        
        
        public int category_id { get; set; }
        public string category_name { get; set; }

        public categories()
        {
        }

        public categories(int category_id, string category_name)
        {
            this.category_id = category_id;
            this.category_name = category_name;
        }
    }
}