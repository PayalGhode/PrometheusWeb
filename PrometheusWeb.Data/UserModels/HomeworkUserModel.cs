using System;
using System.ComponentModel.DataAnnotations;

namespace PrometheusWeb.Data.UserModels
{
    public class HomeworkUserModel
    {
        public int HomeWorkID { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Minimum length of description should be 10 words max. 150.", MinimumLength = 10)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "DeadLine")]
        public DateTime? Deadline { get; set; }

        [Required]
        [Display(Name = "RequiredDate")]
        public DateTime? ReqTime { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "Minimum length of Long description should be 10 Words and max 150.", MinimumLength = 10)]
        [Display(Name = "LongDescription")]
        public string LongDescription { get; set; }
    }
}
