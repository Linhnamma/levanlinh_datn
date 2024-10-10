using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class PurchaseUserProductSupplier
    {
        public PurchaseOrder purchase_order { get; set; }
        public user user { get; set; }
        public Products product { get; set;  }
        public Supplier supplier { get; set; }

        public PurchaseUserProductSupplier()
        {
        }

        public PurchaseUserProductSupplier(PurchaseOrder purchase_order, user user, Products product, Supplier supplier)
        {
            this.purchase_order = purchase_order;
            this.user = user;
            this.product = product;
            this.supplier = supplier;
        }
    }
}