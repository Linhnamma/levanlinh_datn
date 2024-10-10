using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class SupplierUserProductViewModel
    {
        public List<Supplier> supplier { get; set; }
        public user user { get; set; }
        public Products product { get; set; }

        public SupplierUserProductViewModel()
        {
        }

        public SupplierUserProductViewModel(List<Supplier> supplier, user user, Products product)
        {
            this.supplier = supplier;
            this.user = user;
            this.product = product;
        }
    }
}