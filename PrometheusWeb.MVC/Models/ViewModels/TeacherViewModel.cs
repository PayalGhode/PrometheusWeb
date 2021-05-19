using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrometheusWeb.MVC.Models.ViewModels
{
    public class TeacherViewModel
    {
        public int TeacherID { get; set; }
        public int FName { get; set; }
        public string LName { get; set; }
        public string UserID { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int MobileNo { get; set; }
        public bool IsAdmin { get; set; }
    }
}