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
    public class UserService : IUserService
    {

        private PrometheusEntities db = null;
        public UserService()
        {
            db = new PrometheusEntities();
        }

        public AdminUserModel DeleteUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return null;
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return new AdminUserModel
            {
                UserID = user.UserID,
                Password = user.Password,
                Role = user.Role,
                SecurityQuestion = user.SecurityQuestion,
                SecurityAnswer = user.SecurityAnswer
            };
        }

        public AdminUserModel GetUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return null;
            }
            AdminUserModel userModel = new AdminUserModel
            {
                UserID = user.UserID,
                Password = user.Password,
                Role = user.Role,
                SecurityQuestion = user.SecurityQuestion,
                SecurityAnswer = user.SecurityAnswer
            };
            return userModel;
        }

        public IQueryable<AdminUserModel> GetUsers()
        {
            return db.Users.Select(item => new AdminUserModel
            {
                UserID = item.UserID,
                Password = item.Password,
                Role = item.Role,
                SecurityQuestion = item.SecurityQuestion,
                SecurityAnswer = item.SecurityAnswer
            });
        }

        public bool AddUser(AdminUserModel adminModel)
        {
            try
            {
                User user = new User
                {
                    UserID = adminModel.UserID,
                    Password = adminModel.Password,
                    Role = adminModel.Role,
                    SecurityQuestion = adminModel.SecurityQuestion,
                    SecurityAnswer = adminModel.SecurityAnswer
                };

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("PK__Users__"))
                {
                    throw new PrometheusWebException("UserID Already Taken!");
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

        public bool UpdateUser(string id, AdminUserModel adminModel)
        {
            User user = new User
            {
                UserID = adminModel.UserID,
                Password = adminModel.Password,
                Role = adminModel.Role,
                SecurityQuestion = adminModel.SecurityQuestion,
                SecurityAnswer = adminModel.SecurityAnswer
            };

            if (id != user.UserID)
            {
                return false;
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsUserExists(id))
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

        public bool IsUserExists(string id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }
    }
}