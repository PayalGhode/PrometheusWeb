using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrometheusWeb.Data.UserModels
{
    [Table("Teacher")]
    public class TeacherUserModel
    {
        public int TeacherID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        
        public string UserID { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime? DOB { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Phone]
        [StringLength(10, ErrorMessage = "The Phone Number should be 10 digits only.", MinimumLength = 10)]
        [RegularExpression(@"^[0-9]{10}$",
         ErrorMessage = "Characters are not allowed and 10 digits only ")]
        public string MobileNo { get; set; }

        public bool? IsAdmin { get; set; }
    }
}
