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

namespace DATN_LEVANLINH.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        public AccountController()
        {
            _httpClient = new HttpClient(); 
        }
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
       

        public ActionResult Login(string user_name, string password)
        {
            string link = "https://localhost:44395/api/default/account?user_name=" + user_name + "&password=" + password;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(user));
            object data = js.ReadObject(response.GetResponseStream());
            user user = data as user;

            if (user != null)
            {
                // Lưu user vào session
                Session["user"] = user;

                // Trả về JSON thông báo thành công
                return Json(new { success = true, role_id = user.role_id }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Trả về JSON thông báo thất bại
                return Json(new { success = false, message = "Invalid username or password" }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Logout()
        {
            // Xóa session người dùng
            Session["user"] = null;

            // Chuyển hướng về trang chủ hoặc trang đăng nhập
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Signup(user model)
        {
            if (ModelState.IsValid)
            {
                model.role_id = 3;  // Đặt RoleId là 1 cho hàm Signup

                var apiUrl = "https://localhost:44395/api/default/themUser";
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Đăng ký thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Đăng ký thất bại. Vui lòng thử lại." });
                }
            }

            return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
        }
        public ActionResult Taikhoan(int id)
        {
            
            string str = string.Format("?id={0}", id);
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


            return View(user);
            

        }

        public ActionResult ThongTinTaikhoan(int id)
        {
           
            string str = string.Format("?id={0}", id);
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


            return PartialView("ThongTinTaiKhoan",user);
            

        }

        public ActionResult DoiMatKhau(int id)
        {
            string str = string.Format("?id={0}", id);
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
            return PartialView("DoiMatKhau",user);
        }
        [HttpPost]
        public async Task<ActionResult> doimatkhau(user user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            using (var client = new HttpClient())
            {
                // Đặt URL của API chỉnh sửa người dùng
                var apiUrl = "https://localhost:44395/api/default/doimatkhau";

                // Chuyển dữ liệu người dùng thành JSON
                var jsonUser = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                try
                {
                    // Gửi yêu cầu PUT tới API
                    HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Nếu chỉnh sửa thành công, chuyển hướng đến trang cần thiết
                        return RedirectToAction("Quanlytaikhoan");
                    }
                    else
                    {
                        // Xử lý lỗi từ API
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        return new HttpStatusCodeResult((int)response.StatusCode, errorMessage);
                    }
                }
                catch (HttpRequestException ex)
                {
                    // Xử lý lỗi khi gửi yêu cầu
                    return new HttpStatusCodeResult(500, "Error connecting to the server: " + ex.Message);
                }
            }
        }

        private ActionResult BadRequest(string v)
        {
            throw new NotImplementedException();
        }

        public ActionResult Quanlytaikhoan()
        {
            string link = "https://localhost:44395/api/default/listUser";
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();


            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(user[]));
            object data = js.ReadObject(response.GetResponseStream());
            user[] user = data as user[];


            if (user == null)
            {
                return HttpNotFound();
            }


            
            return PartialView("Quanlytaikhoan",user);
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
       
        [HttpPost]
        public async Task<ActionResult> XoaUser(int id)
        {
            using (var client = new HttpClient())
            {
                // URL của API xóa người dùng
                var apiUrl = $"https://localhost:44395/api/default/xoaUser/{id}";

                // Gửi yêu cầu DELETE đến API
                HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Nếu xóa thành công, chuyển hướng đến trang cần thiết (ví dụ: Index)
                    return RedirectToAction("Index");
                }
                else
                {
                    // Xử lý lỗi nếu có
                    return new HttpStatusCodeResult(response.StatusCode, "Error deleting user.");
                }
            }
        }

        public ActionResult ThanhToan(int product_id, string product_name, string pic_name, string descriptions, decimal price, int quantity, int users_id, int category_id, int subcategory_id)
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
            UserProductViewModel userProduct = new UserProductViewModel();
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
                
                userProduct = new UserProductViewModel(user, product);
                return View(userProduct);
            }
            else { return HttpNotFound(); }
      
        }
        [HttpPost]
        public async Task<ActionResult> themHD(Oders model)
        {
            if (ModelState.IsValid)
            {
                 

                var apiUrl = "https://localhost:44395/api/default/themHD";
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
    }
}