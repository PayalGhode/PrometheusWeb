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
    public class StudentService : IStudentService
    {

        private PrometheusEntities db = null;
        public StudentService()
        {
            db = new PrometheusEntities();
        }
        public bool AddStudent(StudentUserModel studentModel)
        {
            try
            {
                Student student = new Student
                {
                    StudentID = studentModel.StudentID,
                    FName = studentModel.FName,
                    LName = studentModel.LName,
                    UserID = studentModel.UserID,
                    DOB = studentModel.DOB,
                    Address = studentModel.Address,
                    City = studentModel.City,
                    MobileNo = studentModel.MobileNo
                };

                db.Students.Add(student);
                db.SaveChanges();

                return true;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("UQ__Student__"))
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


        public StudentUserModel DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return null;
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return new StudentUserModel
            {
                StudentID = student.StudentID,
                FName = student.FName,
                LName = student.LName,
                UserID = student.UserID,
                DOB = student.DOB,
                Address = student.Address,
                City = student.City,
                MobileNo = student.MobileNo
            };
        }

        public StudentUserModel GetStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return null;
            }
            StudentUserModel studentUser = new StudentUserModel
            {
                StudentID = student.StudentID,
                FName = student.FName,
                LName = student.LName,
                UserID = student.UserID,
                DOB = student.DOB,
                Address = student.Address,
                City = student.City,
                MobileNo = student.MobileNo
            };
            return studentUser;
        }

        public IQueryable<StudentUserModel> GetStudents()
        {
            return db.Students.Select(item => new StudentUserModel
            {
                StudentID = item.StudentID,
                FName = item.FName,
                LName = item.LName,
                UserID = item.UserID,
                DOB = item.DOB,
                Address = item.Address,
                City = item.City,
                MobileNo = item.MobileNo
            });
        }

        public bool UpdateStudent(int id, StudentUserModel studentModel)
        {
            Student student = new Student
            {
                StudentID = studentModel.StudentID,
                FName = studentModel.FName,
                LName = studentModel.LName,
                UserID = studentModel.UserID,
                DOB = studentModel.DOB,
                Address = studentModel.Address,
                City = studentModel.City,
                MobileNo = studentModel.MobileNo
            };

            if (id != student.StudentID)
            {
                return false;
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsStudentExists(id))
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

        public int GetStudentID(string UserID)
        {
            return db.Students.Where(item => item.UserID.Equals(UserID)).FirstOrDefault().StudentID;
        }

        public bool IsStudentExists(int id)
        {
            return db.Students.Count(e => e.StudentID == id) > 0;
        }
    }
}