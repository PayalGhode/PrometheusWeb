using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Data.UserModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using PrometheusWeb.Data;
using System.Data.Entity;
using PrometheusWeb.Exceptions;

namespace PrometheusWeb.Services.Services
{
    public class CourseService : ICourseService
    {
 
        private PrometheusEntities db = null;
        public CourseService()
        {
            db = new PrometheusEntities();
        }
        public bool AddCourse(CourseUserModel courseModel)
        {
            try
            {
                Course course = new Course
                {
                    CourseID = courseModel.CourseID,
                    Name = courseModel.Name,
                    StartDate = courseModel.StartDate,
                    EndDate = courseModel.EndDate
                };

                db.Courses.Add(course);
                db.SaveChanges();

                return true;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("UQ__Course__"))
                {
                    throw new PrometheusWebException("Subject Already Added!");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CourseUserModel DeleteCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return null;
            }

            db.Courses.Remove(course);
            db.SaveChanges();

            return new CourseUserModel
            {
                CourseID = course.CourseID,
                Name = course.Name,
                EndDate = course.EndDate,
                StartDate = course.StartDate
            };
        }

        public CourseUserModel GetCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return null;
            }
            CourseUserModel courseUser = new CourseUserModel
            {
                CourseID = course.CourseID,
                Name = course.Name,
                StartDate = course.StartDate,
                EndDate = course.EndDate
            };
            return courseUser;
        }

        public IQueryable<CourseUserModel> GetCourses()
        {
            return db.Courses.Select(item => new CourseUserModel
            {
                CourseID = item.CourseID,
                Name = item.Name,
                StartDate = item.StartDate,
                EndDate = item.EndDate
            });
        }

        public bool UpdateCourse(int id, CourseUserModel courseModel)
        {
            Course course = new Course
            {
                CourseID = courseModel.CourseID,
                Name = courseModel.Name,
                StartDate = courseModel.StartDate,
                EndDate = courseModel.EndDate
            };

            if (id != course.CourseID)
            {
                return false;
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsCourseExists(id))
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
        public  bool IsCourseExists(int id)
        {
            return db.Courses.Count(e => e.CourseID == id) > 0;
        }
    }
}