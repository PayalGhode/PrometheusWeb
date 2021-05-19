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
    public class AssignmentService: IAssignmentService
    {
        private PrometheusEntities db;

        //Constructor instatiating db Context
        public AssignmentService()
        {
            db = new PrometheusEntities();
        }

        //Method to return All Assignments
        public IQueryable<AssignmentUserModel> GetAssignments()
        {
            return db.Assignments.Select(item => new AssignmentUserModel
            {
                AssignmentID = item.AssignmentID,
                HomeWorkID = item.HomeWorkID,
                CourseID =(int) item.CourseID,
                TeacherID = item.TeacherID
            });
        }
        //Method to return Assignment based on given id
        public AssignmentUserModel GetAssignment(int id)
        {
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return null;
            }
            AssignmentUserModel userModel = new AssignmentUserModel
            {
                AssignmentID = assignment.AssignmentID,
                HomeWorkID = assignment.HomeWorkID,
                CourseID =(int) assignment.CourseID,
                TeacherID = assignment.TeacherID
            };
            return userModel;
        }

        //METHOD: Insert Assignments to db
        public bool AddAssignment(AssignmentUserModel userModel)
        {
            
            Assignment assignment = new Assignment
            {
                AssignmentID = userModel.AssignmentID,
                HomeWorkID = userModel.HomeWorkID,
                CourseID = userModel.CourseID,
                TeacherID = userModel.TeacherID
            };

            
            try
            {
                //save changes to DB
                db.Assignments.Add(assignment);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                //check the constraints if id already assigned
                if (ex.InnerException.InnerException.Message.Contains("UQ__Assignments__"))
                {
                    throw new PrometheusWebException("Assignment Already Assigned");
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


            return true;
        }

        //METHOD: update Assignments based on id and detailed Assignment User Model to db
        public bool UpdateAssignment(int id, AssignmentUserModel userModel)
        {
            Assignment assignment = new Assignment
            {
                AssignmentID = userModel.AssignmentID,
                HomeWorkID = userModel.HomeWorkID,
                CourseID = userModel.CourseID,
                TeacherID = userModel.TeacherID
            };

            if (id != assignment.AssignmentID)
            {
                return false;
            }

            try
            {
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsAssignmentExists(id))
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

        //METHOD: Delete Assignment from db
        public AssignmentUserModel DeleteAssignment(int id)
        {
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return null;
            }
            try
            {
                db.Assignments.Remove(assignment);
                db.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
            return new AssignmentUserModel
            {
                AssignmentID = assignment.AssignmentID,
                HomeWorkID = assignment.HomeWorkID,
                CourseID =(int) assignment.CourseID,
                TeacherID = assignment.TeacherID
            };
        }

        //METHOD: Returns bool value based on if Assignment with given id exists in db
        public bool IsAssignmentExists(int id)
        {
            return db.Assignments.Count(e => e.AssignmentID == id) > 0;
        }

    }
}