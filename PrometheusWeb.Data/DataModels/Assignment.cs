namespace PrometheusWeb.Data.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assignment")]
    public partial class Assignment
    {
        public int AssignmentID { get; set; }

        public int? HomeWorkID { get; set; }

        public int? TeacherID { get; set; }

        public int? CourseID { get; set; }

        public virtual Course Course { get; set; }

        public virtual Homework Homework { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
