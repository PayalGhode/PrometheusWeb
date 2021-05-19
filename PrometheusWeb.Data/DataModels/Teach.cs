namespace PrometheusWeb.Data.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Teach
    {
        [Key]
        public int TeacherCourseID { get; set; }

        public int? TeacherID { get; set; }

        public int? CourseID { get; set; }

        public virtual Course Course { get; set; }
    }
}
