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
using System.Web.Http;

namespace PrometheusWeb.Services.Services
{
    public class TeachesService : ITeachesService
    {
        private PrometheusEntities db = null;
        public TeachesService()
        {
            db = new PrometheusEntities();
        }

        [Authorize(Roles = "admin,teacher")]
        public bool AddTeacherCourse(TeacherCourseUserModel teacherCourseModel)
        {
            try
            {
                Teach teach = new Teach
                {
                    CourseID = teacherCourseModel.CourseID,
                    TeacherID = teacherCourseModel.TeacherID
                };

                db.Teaches.Add(teach);
                db.SaveChanges();

                return true;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("UQ__Teaches__"))
                {
                    throw new PrometheusWebException("Course Already Selected For Teaching!");
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

        public TeacherCourseUserModel DeleteTeacherCourse(int id)
        {
            Teach teach = db.Teaches.Find(id);
            if (teach == null)
            {
                return null;
            }

            db.Teaches.Remove(teach);
            db.SaveChanges();

            return new TeacherCourseUserModel
            {
                TeacherCourseID = teach.TeacherCourseID,
                TeacherID = teach.TeacherID,
                CourseID = teach.CourseID
            };

        }

        public IQueryable<TeacherCourseUserModel> GetTeacherCourses()
        {
            return db.Teaches.Select(item => new TeacherCourseUserModel
            {
                CourseID = item.CourseID,
                TeacherCourseID = item.TeacherCourseID,
                TeacherID = item.TeacherID
            });
        }

        public TeacherCourseUserModel GetTeacherCourses(int id)
        {
            Teach teachObj = db.Teaches.Find(id);
            if (teachObj == null)
            {
                return null;
            }
            TeacherCourseUserModel teach = new TeacherCourseUserModel
            {
                CourseID = teachObj.CourseID,
                TeacherID = teachObj.TeacherID,
            };
            return teach;
        }

        public bool IsTeachesExists(int id)
        {
            return db.Teaches.Count(e => e.TeacherCourseID == id) > 0;
        }

        public bool UpdateTeacherCourses(int id, TeacherCourseUserModel teacherCourseModel)
        {
            Teach teach = new Teach
            {
                TeacherCourseID = teacherCourseModel.TeacherCourseID,
                CourseID = teacherCourseModel.CourseID,
                TeacherID = teacherCourseModel.TeacherID
            };

            if (id != teach.TeacherCourseID)
            {
                return false;
            }

            db.Entry(teach).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsTeachesExists(id))
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
    }
}