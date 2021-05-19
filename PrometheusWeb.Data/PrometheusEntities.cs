using PrometheusWeb.Data.DataModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PrometheusWeb.Data
{
    public partial class PrometheusEntities : DbContext
    {
        public PrometheusEntities()
            : base("name=PrometheusEntities")
        {
        }

        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Homework> Homework { get; set; }
        public virtual DbSet<HomeworkPlan> HomeworkPlans { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Teach> Teaches { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Assignments)
                .WithOptional(e => e.Course)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Homework>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Homework>()
                .Property(e => e.LongDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Homework>()
                .HasMany(e => e.Assignments)
                .WithOptional(e => e.Homework)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Homework>()
                .HasMany(e => e.HomeworkPlans)
                .WithOptional(e => e.Homework)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Student>()
                .Property(e => e.FName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.LName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.MobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.FName)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.LName)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.MobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Assignments)
                .WithOptional(e => e.Teacher)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.SecurityQuestion)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.SecurityAnswer)
                .IsUnicode(false);
        }
    }
}
