using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DATN_LEVANLINH.Models
{
    public class Products
    {
        public int product_id { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string product_name { get; set; }
        [DisplayName("Ảnh")]
        public string pic_name { get; set; }
        [DisplayName("Mô tả")]
        public string descriptions { get; set; }
        [DisplayName("Giá")]
        public decimal price { get; set; }
        [DisplayName("Số lượng")]
        public int quantity { get; set; }
        [DisplayName("Mã người bán")]
        public int users_id { get; set; }
        [DisplayName("Loại sản phẩm")]
        public int category_id { get; set; }
        public int subcategory_id { get; set; }
        public Products()
        {

        }

        public Products(int product_id, string product_name, string pic_name, string descriptions, int price, int quantity, int users_id, int category_id, int subcategory)
        {
            this.product_id = product_id;
            this.product_name = product_name;
            this.pic_name = pic_name;
            this.descriptions = descriptions;
            this.price = price;
            this.quantity = quantity;
            this.users_id = users_id;
            this.category_id = category_id;
            this.subcategory_id = subcategory_id;
        }
    }
}