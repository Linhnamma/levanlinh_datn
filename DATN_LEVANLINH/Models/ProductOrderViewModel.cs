using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class ProductOrderViewModel
    {
        public Oders order { get; set; }
        public Products product { get; set; }

        public ProductOrderViewModel()
        {
        }

        public ProductOrderViewModel(Oders order, Products product)
        {
            this.order = order;
            this.product = product;
        }
    }
}