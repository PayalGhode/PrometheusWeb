using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrometheusWeb.MVC.Models.ViewModels
{
    public class EnrolledCourse
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
