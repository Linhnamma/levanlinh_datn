using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class UserProductViewModel
    {
        public user user { get; set; }
        public Products product { get; set; }

        public UserProductViewModel()
        {
        }

        public UserProductViewModel(user user, Products product)
        {
            this.user = user;
            this.product = product;
        }
    }
}