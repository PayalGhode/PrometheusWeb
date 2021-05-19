using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PrometheusWeb.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            if (identity.IsAuthenticated)
            {
                var Role = identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value).FirstOrDefault();
                if (Role.Equals("admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (Role.Equals("teacher"))
                {
                    return RedirectToAction("Index", "Teacher");
                }
                else if (Role.Equals("student"))
                {
                    return RedirectToAction("Index", "Student");
                }
            }

            return View();
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