using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrometheusWeb.Data.UserModels
{
    [Table("Course")]
    public class CourseUserModel
    {
        [Display(Name = "Course ID")]
        public int CourseID { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
    }
}
