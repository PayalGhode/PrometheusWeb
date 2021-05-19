using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrometheusWeb.MVC.Models.ViewModels
{
    public class ExtendedHomeworkPlan
    {
        public int StudentID { get; set; }
        public int HomeworkID { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime ReqTime { get; set; }
        public int PriorityLevel { get; set; }
        public bool isCompleted { get; set; }
    }
}
