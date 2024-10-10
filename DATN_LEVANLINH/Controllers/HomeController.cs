using DATN_LEVANLINH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Web.Mvc;

namespace DATN_LEVANLINH.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string link = "https://localhost:44395/api/default/listSP" ;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Products[]));
            object data = js.ReadObject(response.GetResponseStream());
            Products[] product = data as Products[];

            var products = product.ToList();
            return View(products);
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}