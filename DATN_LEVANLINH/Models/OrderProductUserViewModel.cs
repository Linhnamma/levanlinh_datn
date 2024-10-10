using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATN_LEVANLINH.Models;
namespace DATN_LEVANLINH.Models
{
    public class OrderProductUserViewModel
    {
        public Oders order { get; set; }
        public Products product { get; set; }
        public user user { get; set; }

        public OrderProductUserViewModel()
        {
        }

        public OrderProductUserViewModel(Oders order, Products product, user user)
        {
            this.order = order;
            this.product = product;
            this.user = user;
        }
    }
}