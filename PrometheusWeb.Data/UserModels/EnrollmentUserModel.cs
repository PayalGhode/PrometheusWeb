namespace PrometheusWeb.Data.UserModels
{
    public class EnrollmentUserModel
    {
        public int EnrollmentID { get; set; }
        public int? StudentID { get; set; }
        public int? CourseID { get; set; }
        public CourseUserModel Course { get; set; }
    }
}
