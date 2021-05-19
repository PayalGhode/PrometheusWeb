using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Data.UserModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using PrometheusWeb.Data;
using System.Data.Entity;
using System.Web.Http;

namespace PrometheusWeb.Services.Services
{
    public class HomeworkService : IHomeworkService
    {
        private PrometheusEntities db = null;

        public HomeworkService()
        {
            db = new PrometheusEntities();
        }

        //add homework
        [Authorize(Roles="admin,teacher")]
        public bool AddHomework(HomeworkUserModel homeworkModel)
        {
            try
            {
                Homework homework = new Homework
                {
                    HomeWorkID = homeworkModel.HomeWorkID,
                    Description = homeworkModel.Description,
                    Deadline = homeworkModel.Deadline,
                    ReqTime = homeworkModel.ReqTime,
                    LongDescription = homeworkModel.LongDescription
                };
                db.Homework.Add(homework);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                throw;
            }          
            
        }

        [Authorize(Roles = "admin,teacher")]
        public HomeworkUserModel DeleteHomework(int id)
        {
            Homework homework = db.Homework.Find(id);
            if (homework == null)
            {
                return null;
            }

            db.Homework.Remove(homework);
            db.SaveChanges();

            return new HomeworkUserModel
            {
                HomeWorkID = homework.HomeWorkID,
                Description = homework.Description,
                Deadline = homework.Deadline,
                ReqTime = homework.ReqTime,
                LongDescription = homework.LongDescription
            };
        }

        public HomeworkUserModel GetHomework(int id)
        {
            try
            { 
                Homework homework = db.Homework.Find(id);
                if (homework == null)
                {
                    return null;
                }
                return new HomeworkUserModel
                {
                    HomeWorkID = homework.HomeWorkID,
                    Description = homework.Description,
                    Deadline = homework.Deadline,
                    ReqTime = homework.ReqTime,
                    LongDescription = homework.LongDescription
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<HomeworkUserModel> GetHomeworks()
        {
            return db.Homework.Select(item => new HomeworkUserModel
            {
                HomeWorkID = item.HomeWorkID,
                Description = item.Description,
                Deadline = item.Deadline,
                ReqTime = item.ReqTime,
                LongDescription = item.LongDescription
            });
        }

        public bool IsHomeworkExists(int id)
        {
            return db.Homework.Count(e => e.HomeWorkID == id) > 0;
        }

        [Authorize(Roles = "admin,teacher")]
        public bool UpdateHomework(int id, HomeworkUserModel homeworkModel)
        {
            Homework homework = new Homework
            {
                HomeWorkID = homeworkModel.HomeWorkID,
                Description = homeworkModel.Description,
                Deadline = homeworkModel.Deadline,
                ReqTime = homeworkModel.ReqTime,
                LongDescription = homeworkModel.LongDescription
            };

            if (id != homework.HomeWorkID)
            {
                return false;
            }

            db.Entry(homework).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsHomeworkExists(id))
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