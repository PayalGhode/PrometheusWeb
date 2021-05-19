namespace PrometheusWeb.Data.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Homework
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Homework()
        {
            Assignments = new HashSet<Assignment>();
            HomeworkPlans = new HashSet<HomeworkPlan>();
        }

        public int HomeWorkID { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public DateTime? Deadline { get; set; }

        public DateTime? ReqTime { get; set; }

        [Column(TypeName = "text")]
        public string LongDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assignment> Assignments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HomeworkPlan> HomeworkPlans { get; set; }
    }
}
