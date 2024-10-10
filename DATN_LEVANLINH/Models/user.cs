using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class user
    {
        public int users_id { get; set; }


        public string name { get; set; }


        public string full_name { get; set; }

        public string address { get; set; }

        public int? phone { get; set; }

        public string email { get; set; }

        public int role_id { get; set; }
        public string user_name { get; set; }


        public string password { get; set; }


        public user()
        {
        }

        public user(int users_id, string name, string full_name, string address, int? phone, string email, int role_id, string user_name, string password)
        {
            this.users_id = users_id;
            this.name = name;
            this.full_name = full_name;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.role_id = role_id;
            this.user_name = user_name;
            this.password = password;
        }
    }
}