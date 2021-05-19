using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PrometheusWeb.Data;
using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.Exceptions;
using PrometheusWeb.Services.Services;

namespace PrometheusWeb.Services.Controllers
{
    public class StudentsController : ApiController
    {
        private IStudentService _studentService = null;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/Students
        public IQueryable<StudentUserModel> GetStudents()
        {
            return _studentService.GetStudents();
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            StudentUserModel student = _studentService.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, StudentUserModel studentModel)
        {
            bool result;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                result = _studentService.UpdateStudent(id, studentModel);
            }
            catch (Exception)
            {
                if (!_studentService.IsStudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(HttpStatusCode.InternalServerError);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Students
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent(StudentUserModel studentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = _studentService.AddStudent(studentModel);
                if (result)
                    return CreatedAtRoute("DefaultApi", new { id = studentModel.MobileNo }, studentModel);
                else
                    return StatusCode(HttpStatusCode.InternalServerError);
            }
            catch (PrometheusWebException)
            {
                return StatusCode(HttpStatusCode.Conflict);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            StudentUserModel course = _studentService.DeleteStudent(id);

            return Ok(course);
        }

        // GET: api/StudentID
        [HttpGet]
        [Route("api/Student/GetID")]
        [ResponseType(typeof(int))]
        public IHttpActionResult GetStudentID(string userID)
        {
            try
            {
                int StudentID = _studentService.GetStudentID(userID);
                if (StudentID == 0)
                    return NotFound();
                return Ok(StudentID);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}