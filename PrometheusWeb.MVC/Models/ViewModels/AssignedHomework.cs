using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrometheusWeb.MVC.Models.ViewModels
{
    public class AssignedHomework
    {
        public int AssignmentID { get; set; }
        public int HomeWorkID { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime ReqTime { get; set; }
        public string LongDescription { get; set; }
        public string CourseName { get; set; }
        public int TeacherID { get; set; }
        public int CourseID { get; set; }
    }
}
