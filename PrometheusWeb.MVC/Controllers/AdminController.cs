using Newtonsoft.Json;
using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PrometheusWeb.MVC.Controllers
{
    public class AdminController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "https://localhost:44375/";

        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Title = "Admin Index Page";
            return View();
        }
    }
}