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
    public class TeacherService : ITeacherService
    {

        private PrometheusEntities db = null;
        public TeacherService()
        {
            db = new PrometheusEntities();
        }
        public bool AddTeacher(TeacherUserModel teacherModel)
        {
            try
            {
                Teacher teacher = new Teacher
                {
                    TeacherID = teacherModel.TeacherID,
                    FName = teacherModel.FName,
                    LName = teacherModel.LName,
                    UserID = teacherModel.UserID,
                    DOB = teacherModel.DOB,
                    Address = teacherModel.Address,
                    City = teacherModel.City,
                    MobileNo = teacherModel.MobileNo,
                    IsAdmin = teacherModel.IsAdmin
                };

                db.Teachers.Add(teacher);
                db.SaveChanges();

                return true;
            }

            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("UQ__Teacher__"))
                {
                    throw new PrometheusWebException("Phone No. Already used!");
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

        public TeacherUserModel DeleteTeacher(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return null;
            }

            db.Teachers.Remove(teacher);
            db.SaveChanges();

            return new TeacherUserModel
            {
                TeacherID = teacher.TeacherID,
                FName = teacher.FName,
                LName = teacher.LName,
                UserID = teacher.UserID,
                DOB = teacher.DOB,
                Address = teacher.Address,
                City = teacher.City,
                MobileNo = teacher.MobileNo,
                IsAdmin = teacher.IsAdmin
            };
        }

        public TeacherUserModel GetTeacher(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return null;
            }
            TeacherUserModel teacherUser = new TeacherUserModel
            {
                TeacherID = teacher.TeacherID,
                FName = teacher.FName,
                LName = teacher.LName,
                UserID = teacher.UserID,
                DOB = teacher.DOB,
                Address = teacher.Address,
                City = teacher.City,
                MobileNo = teacher.MobileNo,
                IsAdmin = teacher.IsAdmin
            };
            return teacherUser;
        }

        public IQueryable<TeacherUserModel> GetTeachers()
        {
            return db.Teachers.Select(item => new TeacherUserModel
            {
                TeacherID = item.TeacherID,
                FName = item.FName,
                LName = item.LName,
                UserID = item.UserID,
                DOB = item.DOB,
                Address = item.Address,
                City = item.City,
                MobileNo = item.MobileNo,
                IsAdmin = item.IsAdmin
            });
        }

        public bool UpdateTeacher(int id, TeacherUserModel teacherModel)
        {
            Teacher teacher = new Teacher
            {
                TeacherID = teacherModel.TeacherID,
                FName = teacherModel.FName,
                LName = teacherModel.LName,
                UserID = teacherModel.UserID,
                DOB = teacherModel.DOB,
                Address = teacherModel.Address,
                City = teacherModel.City,
                MobileNo = teacherModel.MobileNo,
                IsAdmin = teacherModel.IsAdmin
            };

            if (id != teacher.TeacherID)
            {
                return false;
            }

            db.Entry(teacher).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsTeacherExists(id))
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

        public int GetTeacherID(string UserID)
        {
            try
            {
                int id = db.Teachers.Where(item => item.UserID.Equals(UserID)).FirstOrDefault().TeacherID;
                return id;
            }
            catch
            {
                throw new PrometheusWebException("User Not Found!");
            }
        }

        public bool IsTeacherExists(int id)
        {
            return db.Teachers.Count(e => e.TeacherID == id) > 0;
        }
    }
}