using DATN_LEVANLINH.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DATN_LEVANLINH.Controllers
{
    public class ManagerController : Controller
    {
        private readonly HttpClient _httpClient;
        public ManagerController()
        {
            _httpClient = new HttpClient();
        }
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuanLySanPham()
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
                productCategorySubcategory = new ProductCategorySubcategoryViewModel(product.ToList(), categorySubcategories);


            }
            return PartialView(productCategorySubcategory);
        }

        public ActionResult QuanLyTaiKhoan()
        {
            string link = "https://localhost:44395/api/default/listRole";
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();


            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(User_role[]));
            object data = js.ReadObject(response.GetResponseStream());
            User_role[] role = data as User_role[];


            if (role == null)
            {
                return HttpNotFound();
            }
            return PartialView("QuanLyTaiKhoan", role.ToList());
        }
        public ActionResult HienThiNguoiDungTheoQuyen(int id)
        {
            string link = "https://localhost:44395/api/default/userTheoQuyen?role=" + id;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();


            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(user[]));
            object data = js.ReadObject(response.GetResponseStream());
            user[] users = data as user[];


            if (users == null)
            {
                return HttpNotFound();
            }


            return PartialView("HienThiNguoiDungTheoQuyen", users.ToList());
        }

        public ActionResult XemSanPhamTheoDanhMuc(int? category, int? subcategory)
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


            return PartialView("HienThiSanPhamTheoDanhMuc", product.ToList());

        }
        public ActionResult HienThiSanPhamTheoDM(int? category, int? subcategory)
        {
            try
            {
                string linksp = "https://localhost:44395/api/default/listSPtheoDMSP?category=" + category + "&subcategory=" + subcategory;
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
                            subcategories[] subcategories = data1 as subcategories[];
                            if (subcategories != null)
                            {
                                categorySubcategories.Add(new CategorySubcategoryViewModel(item, subcategories.ToList()));
                            }
                        }
                    }
                    productCategorySubcategory = new ProductCategorySubcategoryViewModel(product.ToList(), categorySubcategories);


                }
                return PartialView("HienThiSanPhamTheoDM", productCategorySubcategory);
            }
            catch (Exception ex)
            {
                // Log chi tiết lỗi và trả về thông tin lỗi 500
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(500, "Internal Server Error: " + ex.Message);
            }

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
                products.users_id = 1;
                // Đặt URL của API chỉnh sửa người dùng
                var apiUrl = "https://localhost:44395/api/default/chinhSuaProduct";

                // Chuyển dữ liệu người dùng thành JSON
                var jsonUser = JsonConvert.SerializeObject(products);
                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                // Gửi yêu cầu PUT tới API
                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Thêm sản phẩm thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Thêm sản phẩm thất bại. Vui lòng thử lại." });
                }
            }
        }
        [HttpPost]
        public async Task<ActionResult> XoaProduct(int id)
        {
            using (var client = new HttpClient())
            {
                // URL của API xóa người dùng

                string str = string.Format("?productId={0}", id);
                string apiUrl = "https://localhost:44395/api/default/delete" + str;
                // Gửi yêu cầu DELETE đến API
                HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Xóa sản phẩm thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Xóa sản phẩm thất bại. Vui lòng thử lại." });
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> ChinhSuaUser(user user)
        {
            using (var client = new HttpClient())
            {
                // Đặt URL của API chỉnh sửa người dùng
                var apiUrl = "https://localhost:44395/api/default/chinhSuaUser";

                // Chuyển dữ liệu người dùng thành JSON
                var jsonUser = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                // Gửi yêu cầu PUT tới API
                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Chỉnh sửa thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Xóa  thất bại. Vui lòng thử lại." });
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> XoaUser(int id)
        {
            using (var client = new HttpClient())
            {
                // URL của API xóa người dùng

                string str = string.Format("?id={0}", id);
                string apiUrl = "https://localhost:44395/api/default/xoaUser" + str;
                // Gửi yêu cầu DELETE đến API
                HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Xóa  thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Xóa  thất bại. Vui lòng thử lại." });
                }
            }
        }


        public ActionResult DoanhSoBanHang()
        {
            return PartialView("DoanhSoBanHang");
        }
        public ActionResult DoanhSoBanHangTheoThoiGian(int id)
        {
            string linksp = "https://localhost:44395/api/default/listSP";
            HttpWebRequest requestsp = HttpWebRequest.CreateHttp(linksp);
            WebResponse responsesp = requestsp.GetResponse();


            DataContractJsonSerializer jssp = new DataContractJsonSerializer(typeof(Products[]));
            object datasp = jssp.ReadObject(responsesp.GetResponseStream());
            Products[] product = datasp as Products[];

            int sosanpham = 0;
            if (product == null)
            {
                return HttpNotFound();
            }
            foreach (var sp in product)
            {
                sosanpham++;
            }
            ViewBag.Sosanpham = sosanpham;
            string link = "https://localhost:44395/api/default/DonHangTheoNgay?id=" + id;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();

            // Sử dụng StreamReader để đọc phản hồi từ response
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var jsonResponse = reader.ReadToEnd();

                // Sử dụng JsonConvert để deserialize JSON
                List<Oders> orders = JsonConvert.DeserializeObject<List<Oders>>(jsonResponse);

                if (orders == null)
                {
                    return HttpNotFound();
                }
                decimal total_amount = 0;
                int count = 0;
                int quantity = 0;
                foreach (var item in orders)
                {
                    total_amount += item.total_amount;
                    quantity += item.quantity;
                    count++;
                }
                ViewBag.TotalAmount = total_amount;
                ViewBag.Quantity = quantity;
                ViewBag.Count = count;
                return PartialView("DoanhSoBanHangTheoThoiGian", orders.ToList());
            }
        }

        public ActionResult HoaDon()
        {
            return PartialView("HoaDon");
        }
        public ActionResult DanhSachHoaDon(int id)
        {
            ViewBag.ID = id;
            string link = "https://localhost:44395/api/default/listDHtheotinhtrang?id=" + id;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            var orderProductUser = new List<OrderProductUserViewModel>();
            // Sử dụng StreamReader để đọc phản hồi từ response
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var jsonResponse = reader.ReadToEnd();

                // Sử dụng JsonConvert để deserialize JSON
                List<Oders> orders = JsonConvert.DeserializeObject<List<Oders>>(jsonResponse);

                foreach (var item in orders)
                {
                    string str = string.Format("?id={0}", item.product_id);
                    string link1 = "https://localhost:44395/api/default/SPtheoId" + str;
                    HttpWebRequest request1 = HttpWebRequest.CreateHttp(link1);
                    Products product = new Products();
                    using (WebResponse response1 = request1.GetResponse())
                    {
                        DataContractJsonSerializer js1 = new DataContractJsonSerializer(typeof(Products));
                        object data1 = js1.ReadObject(response1.GetResponseStream());
                        Products products = data1 as Products;
                        if (product != null)
                        {
                            product = products;
                        }
                    }

                    string str1 = string.Format("?id={0}", item.users_id);
                    string link2 = "https://localhost:44395/api/default/usertheoId" + str1;
                    HttpWebRequest request2 = HttpWebRequest.CreateHttp(link2);
                    user user = new user();
                    using (WebResponse response2 = request2.GetResponse())
                    {
                        DataContractJsonSerializer js2 = new DataContractJsonSerializer(typeof(user));
                        object data2 = js2.ReadObject(response2.GetResponseStream());
                        user users = data2 as user;
                        if (users != null)
                        {
                            user = users;
                        }
                    }
                    orderProductUser.Add(new OrderProductUserViewModel(item, product, user));
                }

            }
            return PartialView("DanhSachHoaDon", orderProductUser);

        }


        [HttpPost]
        public async Task<ActionResult> ChinhSuaHD(Oders order)
        {
            using (var client = new HttpClient())
            {
                // Đặt URL của API chỉnh sửa người dùng
                var apiUrl = "https://localhost:44395/api/default/chinhSuaHD";

                // Chuyển dữ liệu người dùng thành JSON
                var jsonUser = JsonConvert.SerializeObject(order);
                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                // Gửi yêu cầu PUT tới API
                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Thành công" });
                }
                else
                {
                    return Json(new { success = false, message = " Vui lòng thử lại." });
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> XoaDH(int id)
        {
            using (var client = new HttpClient())
            {
                // URL của API xóa người dùng

                string str = string.Format("?id={0}", id);
                string apiUrl = "https://localhost:44395/api/default/xoaDH" + str;
                // Gửi yêu cầu DELETE đến API
                HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Xóa  thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Xóa  thất bại. Vui lòng thử lại." });
                }
            }
        }
        [HttpPost]
        public async Task<ActionResult> ChinhSuaSoLuongSanPham(Products products)
        {
            using (var client = new HttpClient())
            {
                // Đặt URL của API chỉnh sửa người dùng
                var apiUrl = "https://localhost:44395/api/default/chinhSuaSoLuongSanPham";

                // Chuyển dữ liệu người dùng thành JSON
                var jsonProduct = JsonConvert.SerializeObject(products);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                // Gửi yêu cầu PUT tới API
                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Thất bại. Vui lòng thử lại." });
                }
            }

        }
        [HttpPost]
        public async Task<ActionResult> HoanLaiSoLuongSanPham(Products products)
        {
            using (var client = new HttpClient())
            {
                // Đặt URL của API chỉnh sửa người dùng
                var apiUrl = "https://localhost:44395/api/default/hoanlaiSoLuongSanPham";

                // Chuyển dữ liệu người dùng thành JSON
                var jsonProduct = JsonConvert.SerializeObject(products);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                // Gửi yêu cầu PUT tới API
                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Thất bại. Vui lòng thử lại." });
                }
            }


        }

        public ActionResult HoaDonNhap()
        {
            string link = "https://localhost:44395/api/default/listNCC";
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();


            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Supplier[]));
            object data = js.ReadObject(response.GetResponseStream());
            Supplier[] supplier = data as Supplier[];


            if (supplier == null)
            {
                return HttpNotFound();
            }
            return PartialView("HoaDonNhap", supplier.ToList());
        }

        public ActionResult DanhSachHoaDonNhap(int? id, int? ncc)
        {
            string link = "https://localhost:44395/api/default/listHDN?id=" + id + "&supplier_id=" + ncc;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);

            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string jsonResponse = reader.ReadToEnd();
                // Deserialize using JsonConvert
                List<PurchaseOrder> purchaseOrders = JsonConvert.DeserializeObject<List<PurchaseOrder>>(jsonResponse);

                

                return PartialView("DanhSachHoaDonNhap", purchaseOrders);
            }
        }
        public ActionResult NhapSanPham(int? id, int? ncc)
        {
            string link = "https://localhost:44395/api/default/listHDN?id=" + id + "&supplier_id=" + ncc;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);

            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string jsonResponse = reader.ReadToEnd();
                // Deserialize using JsonConvert
                List<PurchaseOrder> purchaseOrders = JsonConvert.DeserializeObject<List<PurchaseOrder>>(jsonResponse);



                return PartialView("DanhSachHoaDonNhap", purchaseOrders);
            }
        }

        public ActionResult NhapHang()
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

                return PartialView("NhapHang", categorySubcategories);
            }
           
        }

        public ActionResult NhapHangTheoDM(int?category, int? subcategory)
        {
            try
            {
                string linksp = "https://localhost:44395/api/default/listSPtheoDMSP?category=" + category + "&subcategory=" + subcategory;
                HttpWebRequest requestsp = HttpWebRequest.CreateHttp(linksp);
                WebResponse responsesp = requestsp.GetResponse();


                DataContractJsonSerializer jssp = new DataContractJsonSerializer(typeof(Products[]));
                object datasp = jssp.ReadObject(responsesp.GetResponseStream());
                Products[] product = datasp as Products[];


                if (product == null)
                {
                    return HttpNotFound();
                }


                
                return PartialView("NhapHangTheoDM", product.ToList());
            }
            catch (Exception ex)
            {
                // Log chi tiết lỗi và trả về thông tin lỗi 500
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(500, "Internal Server Error: " + ex.Message);
            }
        }
        public ActionResult Timkiemsanpham(int? id, string keyword,int style)
        {
            if (style == 1)
            {
                
                string str = string.Format("?id={0}", id);
                string link = "https://localhost:44395/api/default/SPtheoId" + str;
                HttpWebRequest request = HttpWebRequest.CreateHttp(link);
                WebResponse response = request.GetResponse();
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Products));
                object data = js.ReadObject(response.GetResponseStream());
                Products product = data as Products;
                List<Products> listpro = new List<Products>();
                listpro.Add(product);
                return PartialView("Timkiemsanpham", listpro);
            }
            else if (style == 2)
            {
                string linksp = "https://localhost:44395/api/default/searchProducts?keyword=" + keyword;
                HttpWebRequest requestsp = HttpWebRequest.CreateHttp(linksp);
                WebResponse responsesp = requestsp.GetResponse();
                DataContractJsonSerializer jssp = new DataContractJsonSerializer(typeof(Products[]));
                object datasp = jssp.ReadObject(responsesp.GetResponseStream());
                Products[] products = datasp as Products[];



                return PartialView("Timkiemsanpham", products.ToList());
            }

            else
            {
                return HttpNotFound();
            }
            
            


        }
        public ActionResult ChiTietHoaDonNhap(int product_id, string product_name, string pic_name, string descriptions, decimal price, int quantity, int users_id, int category_id, int subcategory_id)
        {
            var product = new Products
            {
                product_id = product_id,
                product_name = product_name,
                pic_name = pic_name,
                descriptions = descriptions,
                price = price,
                quantity = quantity,
                users_id = users_id,
                category_id = category_id,
                subcategory_id = subcategory_id
            };
            var userData = Session["user"] as user;
            SupplierUserProductViewModel userProduct = new SupplierUserProductViewModel();
            if (userData != null)
            {
                string str = string.Format("?id={0}", userData.users_id);
                string link = "https://localhost:44395/api/default/usertheoId" + str;
                HttpWebRequest request = HttpWebRequest.CreateHttp(link);
                WebResponse response = request.GetResponse();


                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(user));
                object data = js.ReadObject(response.GetResponseStream());
                user user = data as user;


                if (user == null)
                {
                    return HttpNotFound();
                }
                string link1 = "https://localhost:44395/api/default/listSupplier" ;
                HttpWebRequest request1 = HttpWebRequest.CreateHttp(link1);
                WebResponse response1 = request1.GetResponse();


                DataContractJsonSerializer js1 = new DataContractJsonSerializer(typeof(Supplier[]));
                object data1 = js1.ReadObject(response1.GetResponseStream());
                Supplier[] supplier = data1 as Supplier[];


               
                userProduct = new SupplierUserProductViewModel(supplier.ToList(),user, product);
                return View(userProduct);
            }
            else { return HttpNotFound(); }
        }
        [HttpPost]
        public async Task<ActionResult> themHDN(PurchaseOrder model)
        {
            if (ModelState.IsValid)
            {


                var apiUrl = "https://localhost:44395/api/default/themHDN";
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Thất bại. Vui lòng thử lại." });
                }
            }

            return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
        }

        public ActionResult XemChiTietHoaDonNhap(int purchaseOrder_id, DateTime purchaseOrder_date, int supplier_id, int product_id, int users_id, int quantity, decimal price, decimal totalAmount)
        {
            var model = new PurchaseOrder
            {
                purchaseOrder_id = purchaseOrder_id,
                purchaseOrder_date = purchaseOrder_date,
                supplier_id = supplier_id,
                product_id = product_id,
                users_id = users_id,
                quantity = quantity,
                price = price,
                totalAmount = totalAmount,
            };


            PurchaseUserProductSupplier userProduct = new PurchaseUserProductSupplier();
            string str = string.Format("?id={0}", model.users_id);
            string link = "https://localhost:44395/api/default/usertheoId" + str;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();


            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(user));
            object data = js.ReadObject(response.GetResponseStream());
            user user = data as user;


            if (user == null)
            {
                return HttpNotFound();
            }
            string str1 = string.Format("?id={0}", model.supplier_id);
            string link1 = "https://localhost:44395/api/default/suppliertheoId"+str1;
            HttpWebRequest request1 = HttpWebRequest.CreateHttp(link1);
            WebResponse response1 = request1.GetResponse();


            DataContractJsonSerializer js1 = new DataContractJsonSerializer(typeof(Supplier));
            object data1 = js1.ReadObject(response1.GetResponseStream());
            Supplier supplier = data1 as Supplier;


            if (supplier == null)
            {
                return HttpNotFound();
            }

            string str2 = string.Format("?id={0}", model.product_id);
            string link2 = "https://localhost:44395/api/default/SPtheoId" + str2;
            HttpWebRequest request2 = HttpWebRequest.CreateHttp(link2);
            WebResponse response2 = request2.GetResponse();


            DataContractJsonSerializer js2 = new DataContractJsonSerializer(typeof(Products));
            object data2 = js2.ReadObject(response2.GetResponseStream());
            Products product = data2 as Products;


            if (product == null)
            {
                return HttpNotFound();
            }
            userProduct = new PurchaseUserProductSupplier(model, user, product,supplier);
            return View(userProduct);


        }

        public ActionResult Bieudodoanhso()
        {
            int id = 3;
            string link = "https://localhost:44395/api/default/DonHangTheoNgay?id=" + id;

            try
            {
                HttpWebRequest request = HttpWebRequest.CreateHttp(link);
                WebResponse response = request.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var jsonResponse = reader.ReadToEnd();
                    List<Oders> orders = JsonConvert.DeserializeObject<List<Oders>>(jsonResponse);

                    if (orders == null || !orders.Any())
                    {
                        return HttpNotFound();
                    }

                    // Lấy tháng hiện tại và tạo danh sách tất cả các ngày trong tháng đó
                    DateTime today = DateTime.Now; // Lấy ngày hiện tại
                    DateTime startDate = new DateTime(today.Year, today.Month, 1); // Ngày đầu tiên của tháng hiện tại
                    DateTime endDate = startDate.AddMonths(1).AddDays(-1); // Ngày cuối cùng của tháng hiện tại

                    var allDays = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                             .Select(d => startDate.AddDays(d))
                                             .ToList();

                    // Tạo dữ liệu doanh thu với mặc định doanh thu là 0 nếu không có đơn hàng
                    var revenuePerDay = allDays.Select(date => new
                    {
                        Date = date,
                        TotalAmount = orders.Where(o => o.order_date.Date == date).Sum(o => o.total_amount)
                    }).ToList();

                    // Tạo danh sách ngày và doanh thu để truyền vào View
                    var days = revenuePerDay.Select(r => r.Date.ToString("dd/MM/yyyy")).ToList();
                    var revenues = revenuePerDay.Select(r => r.TotalAmount).ToList();

                    ViewBag.Days = days;
                    ViewBag.Revenues = revenues;

                    return PartialView("Bieudodoanhso");
                }
            }
            catch (WebException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Lỗi khi gọi API");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi");
            }
        }
    }
}