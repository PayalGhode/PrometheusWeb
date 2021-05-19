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
    public class TeachersController : ApiController
    {
        private ITeacherService _teacherService = null;
        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: api/Teachers
        public IQueryable<TeacherUserModel> GetTeachers()
        {
            return _teacherService.GetTeachers();
        }

        // GET: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult GetTeacher(int id)
        {
            TeacherUserModel teacher = _teacherService.GetTeacher(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        // PUT: api/Teachers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacher(int id, TeacherUserModel teacherModel)
        {
            bool result;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                result = _teacherService.UpdateTeacher(id, teacherModel);
            }
            catch (Exception)
            {
                if (!_teacherService.IsTeacherExists(id))
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

        // POST: api/Teachers
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult PostTeacher(TeacherUserModel teacherModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = _teacherService.AddTeacher(teacherModel);
                if (result)
                    return CreatedAtRoute("DefaultApi", new { id = teacherModel.MobileNo }, teacherModel);
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

        // DELETE: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult DeleteTeacher(int id)
        {
            TeacherUserModel teacher = _teacherService.DeleteTeacher(id);

            return Ok(teacher);
        }

        // GET: api/Teacher/GetID
        [HttpGet]
        [Route("api/Teacher/GetID")]
        [ResponseType(typeof(int))]
        public IHttpActionResult GetTeacherID(string userID)
        {
            try
            {
                int TeacherID = _teacherService.GetTeacherID(userID);
                if (TeacherID == 0)
                    return NotFound();
                return Ok(TeacherID);
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