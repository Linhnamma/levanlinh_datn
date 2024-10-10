using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class ProductQuantity
    {
        public int product_id { get; set; }
        public int total_quantity { get; set; }

        public ProductQuantity()
        {
        }

        public ProductQuantity(int product_id, int total_quantity)
        {
            this.product_id = product_id;
            this.total_quantity = total_quantity;
        }
    }
}