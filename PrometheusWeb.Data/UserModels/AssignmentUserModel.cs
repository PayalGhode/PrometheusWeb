using System.ComponentModel.DataAnnotations;

namespace PrometheusWeb.Data.UserModels
{
    public class AssignmentUserModel
    {
        public int AssignmentID { get; set; }

        public int? HomeWorkID { get; set; }

        public int? TeacherID { get; set; }

        [Required]
        public int CourseID { get; set; }
    }
}
