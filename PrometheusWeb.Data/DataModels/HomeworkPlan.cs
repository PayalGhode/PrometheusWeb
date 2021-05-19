namespace PrometheusWeb.Data.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HomeworkPlan")]
    public partial class HomeworkPlan
    {
        public int HomeworkPlanID { get; set; }

        public int? StudentID { get; set; }

        public int? HomeworkID { get; set; }

        public int? PriorityLevel { get; set; }

        public bool? isCompleted { get; set; }

        public virtual Homework Homework { get; set; }

        public virtual Student Student { get; set; }
    }
}
