namespace PrometheusWeb.Data.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Enrollments = new HashSet<Enrollment>();
            HomeworkPlans = new HashSet<HomeworkPlan>();
        }

        public int StudentID { get; set; }

        [StringLength(30)]
        public string FName { get; set; }

        [StringLength(30)]
        public string LName { get; set; }

        [StringLength(30)]
        public string UserID { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        [StringLength(13)]
        public string MobileNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HomeworkPlan> HomeworkPlans { get; set; }

        public virtual User User { get; set; }
    }
}
