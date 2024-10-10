using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class Supplier
    {
        public int supplier_id { get; set; }
   
        public string supplier_name { get; set; }

        public Supplier()
        {
        }

        public Supplier(int supplier_id, string supplier_name)
        {
            this.supplier_id = supplier_id;
            this.supplier_name = supplier_name;
        }
    }
}