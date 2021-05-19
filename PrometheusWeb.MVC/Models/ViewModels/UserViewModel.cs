using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrometheusWeb.MVC.Models.ViewModels
{
    public class UserViewModel
    {
        public int StudentID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserID { get; set; }
        public DateTime? DOB { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
    }
}