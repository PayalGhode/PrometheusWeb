using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrometheusWeb.Data.UserModels
{
    public class HomeworkPlanUserModel
    {
        public int? HomeworkPlanID { get; set; }

        public int? StudentID { get; set; }

        [Required]
        public int? HomeworkID { get; set; }
        [Required]
        public int? PriorityLevel { get; set; }
        [Required]
        public bool? isCompleted { get; set; }

        public HomeworkUserModel Homework { get; set; }
    }
}
