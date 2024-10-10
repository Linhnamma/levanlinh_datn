using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using DATN_LEVANLINH.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace DATN_LEVANLINH.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        public ProductsController()
        {
            _httpClient = new HttpClient();
        }
        // GET: Products
        public ActionResult XemSanPham()
        {
            string link = "https://localhost:44395/api/default/listDM";
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(categories[]));
            object data = js.ReadObject(response.GetResponseStream());
            categories[] categories = data as categories[];

            
            return View(categories.ToList());
        }
        public ActionResult Timkiem(string keyword, int page = 1, int pageSize = 20)
        {
            string link = "https://localhost:44395/api/default/searchProducts?keyword=" + keyword;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Products[]));
            object data = js.ReadObject(response.GetResponseStream());
            Products[] product = data as Products[];

            var products = product.ToList();

            int totalItems = products.Count;
            int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var items = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Keyword = keyword;
           
            return View(items);

        }
        public ActionResult XemSanPhamTheoDanhMucLon(int? category,int? subcategory,int page = 1, int pageSize = 20)
        {
            string link = "https://localhost:44395/api/default/listSPtheoDMSP?category=" + category + "&subcategory=" + subcategory;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Products[]));
            object data = js.ReadObject(response.GetResponseStream());
            Products[] product = data as Products[];

            var products = product.ToList();

            int totalItems = products.Count;
            int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var items = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            ViewBag.Category = category;
            ViewBag.Subcategory = subcategory;
            return View( items);
            
        }
        public ActionResult XemSanPhamTheoDanhMuc(int? subcategory)
        {

            string str = string.Format("?subcategory={0}", subcategory);
            string link = "https://localhost:44395/api/default/listSPtheoSubDM" + str;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();


            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Products[]));
            object data = js.ReadObject(response.GetResponseStream());
            Products[] product = data as Products[];


            if (product == null)
            {
                return HttpNotFound();
            }


            return PartialView("XemSanPhamTheoDanhMuc", product.ToList());
        }
        public ActionResult ChiTietSanPham(int id)
        {
           
            string str = string.Format("?id={0}", id);
            string link = "https://localhost:44395/api/default/SPtheoId" + str;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();

            
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Products));
            object data = js.ReadObject(response.GetResponseStream());
            Products product = data as Products;

            
            if (product == null)
            {
                return HttpNotFound();
            }

            
            return View(product);
        }


        public ActionResult HienThiSanPham()
        {

            string link = "https://localhost:44395/api/default/listDM";
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);

            using (WebResponse response = request.GetResponse())
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(categories[]));
                object data = js.ReadObject(response.GetResponseStream());
                categories[] categories = data as categories[];
                var categorySubcategories = new List<CategorySubcategoryViewModel>();

                foreach (var item in categories)
                {
                    string str = string.Format("?category={0}", item.category_id);
                    string link1 = "https://localhost:44395/api/default/listSubDMTheoDM" + str;
                    HttpWebRequest request1 = HttpWebRequest.CreateHttp(link1);

                    using (WebResponse response1 = request1.GetResponse())
                    {
                        DataContractJsonSerializer js1 = new DataContractJsonSerializer(typeof(subcategories[]));
                        object data1 = js1.ReadObject(response1.GetResponseStream());
                        subcategories[] subcategory = data1 as subcategories[];
                        if (subcategory != null)
                        {
                            categorySubcategories.Add(new CategorySubcategoryViewModel(item, subcategory.ToList()));
                        }
                    }
                }

                return View(categorySubcategories);
            }
        }
        public ActionResult HienThiSanPhamTheoDM(int? category, int? subcategory )
        {
            
            string link = "https://localhost:44395/api/default/listSPtheoDMSP?category=" + category + "&subcategory=" + subcategory;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();


            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Products[]));
            object data = js.ReadObject(response.GetResponseStream());
            Products[] product = data as Products[];


            if (product == null)
            {
                return HttpNotFound();
            }

            
            return PartialView("HienThiSanPhamTheoDM", product.ToList());
        }

        public ActionResult GioHang(int? id)
        {
            id = 1038;
            string str = string.Format("?id={0}", id);
            string link = "https://localhost:44395/api/default/SPtheoId" + str;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();


            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Products));
            object data = js.ReadObject(response.GetResponseStream());
            Products product = data as Products;


            if (product == null)
            {
                return HttpNotFound();
            }


            return View(product);
        }


        public ActionResult DonHang(int? user_id)
        {
            string str = string.Format("?user_id={0}", user_id);
            string link = "https://localhost:44395/api/default/listDHtheouser" + str;

            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string jsonResponse = reader.ReadToEnd();
                Oders[] order = JsonConvert.DeserializeObject<Oders[]>(jsonResponse);

                List<ProductOrderViewModel> ProductOrderViewModel = new List<ProductOrderViewModel>();

                if (order == null)
                {
                    return HttpNotFound();
                }

                foreach (var item in order)
                {
                    string str1 = string.Format("?id={0}", item.product_id);
                    string link1 = "https://localhost:44395/api/default/SPtheoId" + str1;

                    HttpWebRequest request1 = HttpWebRequest.CreateHttp(link1);
                    WebResponse response1 = request1.GetResponse();
                    using (StreamReader reader1 = new StreamReader(response1.GetResponseStream()))
                    {
                        string jsonResponse1 = reader1.ReadToEnd();
                        Products products = JsonConvert.DeserializeObject<Products>(jsonResponse1);
                        ProductOrderViewModel.Add(new ProductOrderViewModel(item, products));
                    }
                }

                return PartialView("DonHang", ProductOrderViewModel);
            }
        }


        public ActionResult Quanlysanpham()
        {
            string linksp = "https://localhost:44395/api/default/listSP";
            HttpWebRequest requestsp = HttpWebRequest.CreateHttp(linksp);
            WebResponse responsesp = requestsp.GetResponse();


            DataContractJsonSerializer jssp = new DataContractJsonSerializer(typeof(Products[]));
            object datasp = jssp.ReadObject(responsesp.GetResponseStream());
            Products[] product = datasp as Products[];


            if (product == null)
            {
                return HttpNotFound();
            }

            string link = "https://localhost:44395/api/default/listDM";
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            var productCategorySubcategory = new ProductCategorySubcategoryViewModel();
            using (WebResponse response = request.GetResponse())
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(categories[]));
                object data = js.ReadObject(response.GetResponseStream());
                categories[] categories = data as categories[];
                var categorySubcategories = new List<CategorySubcategoryViewModel>();

                foreach (var item in categories)
                {
                    string str = string.Format("?category={0}", item.category_id);
                    string link1 = "https://localhost:44395/api/default/listSubDMTheoDM" + str;
                    HttpWebRequest request1 = HttpWebRequest.CreateHttp(link1);

                    using (WebResponse response1 = request1.GetResponse())
                    {
                        DataContractJsonSerializer js1 = new DataContractJsonSerializer(typeof(subcategories[]));
                        object data1 = js1.ReadObject(response1.GetResponseStream());
                        subcategories[] subcategory = data1 as subcategories[];
                        if (subcategory != null)
                        {
                            categorySubcategories.Add(new CategorySubcategoryViewModel(item, subcategory.ToList()));
                        }
                    }
                }
                productCategorySubcategory=new ProductCategorySubcategoryViewModel(product.ToList(), categorySubcategories);

                
            }
            return PartialView(productCategorySubcategory);


        }

        [HttpPost]
        public async Task<ActionResult> ThemSanPham()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files["img_product"];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Image"), fileName);
                    file.SaveAs(path);
                }
            }

            // Lấy thông tin sản phẩm từ FormData
            var productData = Request.Form["product"];
            var product = JsonConvert.DeserializeObject<Products>(productData);

            if (ModelState.IsValid)
            {
                // Đặt RoleId là 1 cho hàm Signup
                product.users_id = 1;
                var apiUrl = "https://localhost:44395/api/default/themSanpham";
                var jsonContent = JsonConvert.SerializeObject(product);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Thêm sản phẩm thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Thêm sản phẩm thất bại. Vui lòng thử lại." });
                }
            }

            return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
        }

        [HttpPost]
        public async Task<ActionResult> ChinhSuaProduct(Products products)
        {
            using (var client = new HttpClient())
            {
                products.users_id=1;
                // Đặt URL của API chỉnh sửa người dùng
                var apiUrl = "https://localhost:44395/api/default/chinhSuaProduct";

                // Chuyển dữ liệu người dùng thành JSON
                var jsonUser = JsonConvert.SerializeObject(products);
                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                // Gửi yêu cầu PUT tới API
                HttpResponseMessage response = await client.PutAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    // Nếu chỉnh sửa thành công, chuyển hướng đến trang cần thiết
                    return RedirectToAction("Quanlytaikhoan");
                }
                else
                {
                    // Xử lý lỗi nếu có
                    return new HttpStatusCodeResult((int)response.StatusCode, "Error updating user.");
                }
                
            }
        }
        public ActionResult SanPhamBanChay()
        {
            string link = "https://localhost:44395/api/default/getUniqueProductIds";
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            List<Products> listpro = new List<Products>();
            using (WebResponse response = request.GetResponse())
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(int[]));
                object data = js.ReadObject(response.GetResponseStream());
                int[] productquantity = data as int[];

                foreach (var item in productquantity)
                {
                    string str = string.Format("?id={0}", item);
                    string link1 = "https://localhost:44395/api/default/SPtheoId" + str;
                    HttpWebRequest request1 = HttpWebRequest.CreateHttp(link1);

                    using (WebResponse response1 = request1.GetResponse())
                    {
                        DataContractJsonSerializer js1 = new DataContractJsonSerializer(typeof(Products));
                        object data1 = js1.ReadObject(response1.GetResponseStream());
                        Products products = data1 as Products;
                        if (products != null)
                        {
                            listpro.Add(products);
                        }
                    }
                }

                return PartialView("SanPhamBanChay", listpro);

            }

        }

        public ActionResult SanPhamMoi()
        {
           
            string link = "https://localhost:44395/api/default/getLatestProductIds";

            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string jsonResponse = reader.ReadToEnd();
                PurchaseOrder[] purchaseorder = JsonConvert.DeserializeObject<PurchaseOrder[]>(jsonResponse);

                List<Products> Product = new List<Products>();

                if (purchaseorder == null)
                {
                    return HttpNotFound();
                }

                foreach (var item in purchaseorder)
                {
                    string str1 = string.Format("?id={0}", item.product_id);
                    string link1 = "https://localhost:44395/api/default/SPtheoId" + str1;

                    HttpWebRequest request1 = HttpWebRequest.CreateHttp(link1);
                    WebResponse response1 = request1.GetResponse();
                    using (StreamReader reader1 = new StreamReader(response1.GetResponseStream()))
                    {
                        string jsonResponse1 = reader1.ReadToEnd();
                        Products products = JsonConvert.DeserializeObject<Products>(jsonResponse1);
                        Product.Add(products);
                    }
                }

                return PartialView("SanPhamMoi", Product);
            }

        }


    }
}