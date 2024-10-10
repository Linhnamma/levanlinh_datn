using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class Oders
    {
        public int order_id { get; set; }

        public int users_id { get; set; }

        public DateTime order_date { get; set; }

        public int discount { get; set; }

        public decimal total_amount { get; set; }

        public int product_id { get; set; }

        public int quantity { get; set; }

       
        public string status { get; set; }
  
        public string payment_status { get; set; }

        public Oders()
        {
        }

        public Oders(int order_id, int users_id, DateTime order_date, int discount, decimal total_amount, int product_id, int quantity, string status, string payment_status)
        {
            this.order_id = order_id;
            this.users_id = users_id;
            this.order_date = order_date;
            this.discount = discount;
            this.total_amount = total_amount;
            this.product_id = product_id;
            this.quantity = quantity;
            this.status = status;
            this.payment_status = payment_status;
        }
    }
}