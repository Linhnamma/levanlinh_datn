using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class PurchaseOrder
    {
        public int purchaseOrder_id { get; set; }

        public DateTime purchaseOrder_date { get; set; }

        public int supplier_id { get; set; }

        public int product_id { get; set; }

        public int users_id { get; set; }

        public int quantity { get; set; }

        public decimal price { get; set; }

        public decimal totalAmount { get; set; }

        public PurchaseOrder()
        {
        }

        public PurchaseOrder(int purchaseOrder_id, DateTime purchaseOrder_date, int supplier_id, int product_id, int users_id, int quantity, decimal price, decimal totalAmount)
        {
            this.purchaseOrder_id = purchaseOrder_id;
            this.purchaseOrder_date = purchaseOrder_date;
            this.supplier_id = supplier_id;
            this.product_id = product_id;
            this.users_id = users_id;
            this.quantity = quantity;
            this.price = price;
            this.totalAmount = totalAmount;
        }
    }
}