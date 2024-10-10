using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class User_role
    {
        public int role_id { get; set; }
        public string role_name { get; set; }

        public User_role()
        {
        }

        public User_role(int role_id, string role_name)
        {
            this.role_id = role_id;
            this.role_name = role_name;
        }
    }
}