using PrometheusWeb.Data;
using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PrometheusWeb.Services.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private PrometheusEntities db = null;

        public EnrollmentService()
        {
            db = new PrometheusEntities();
        }
        public IQueryable<EnrollmentUserModel> GetEnrollments()
        {
            return db.Enrollments.Select(item => new EnrollmentUserModel
            {
                EnrollmentID = item.EnrollmentID,
                CourseID = item.CourseID,
                StudentID = item.StudentID,
                Course = new CourseUserModel
                {
                    CourseID = item.Course.CourseID,
                    Name = item.Course.Name,
                    StartDate = item.Course.StartDate,
                    EndDate = item.Course.EndDate
                }
            });
        }
        public EnrollmentUserModel GetEnrollment(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return null;
            }
            EnrollmentUserModel enrolledUser = new EnrollmentUserModel
            {
                EnrollmentID = enrollment.EnrollmentID,
                CourseID = enrollment.CourseID,
                StudentID = enrollment.StudentID,
                Course = new CourseUserModel
                {
                    CourseID = enrollment.Course.CourseID,
                    Name = enrollment.Course.Name,
                    StartDate = enrollment.Course.StartDate,
                    EndDate = enrollment.Course.EndDate
                }
            };
            return enrolledUser;
        }
        public bool AddEnrollment(EnrollmentUserModel enrollmentModel)
        {
            try
            {
            Enrollment enrollment = new Enrollment
            {

                EnrollmentID = enrollmentModel.EnrollmentID,
                CourseID = enrollmentModel.CourseID,
                StudentID = enrollmentModel.StudentID
            };

            db.Enrollments.Add(enrollment);
            db.SaveChanges();

            return true;
            }
            catch(DbUpdateException ex)
            {
                if(ex.InnerException.InnerException.Message.Contains("UK_EnrollmentOnSIdAndCId"))
                {
                    throw new PrometheusWebException("Already Enrolled!");
                }
                else
                {
                    throw;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        public bool UpdateEnrollment(int id, EnrollmentUserModel enrollmentModel)
        {
            Enrollment enrollment = new Enrollment
            {
                
                EnrollmentID = enrollmentModel.EnrollmentID,
                CourseID = enrollmentModel.CourseID,
                StudentID = enrollmentModel.StudentID
            };

            if (id != enrollment.EnrollmentID)
            {
                return false;
            }

            db.Entry(enrollment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsEnrollmentExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        public EnrollmentUserModel DeleteEnrollment(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return null;
            }

            db.Enrollments.Remove(enrollment);
            db.SaveChanges();

            return new EnrollmentUserModel
            {
                EnrollmentID = enrollment.EnrollmentID,
                CourseID = enrollment.CourseID,
                StudentID = enrollment.StudentID
            };
        }
        public bool IsEnrollmentExists(int id)
        {
            return db.Enrollments.Count(e => e.EnrollmentID == id) > 0;
        }
        
    }
}