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
    public class TeachesController : ApiController
    {
        private ITeachesService _teachesService = null;

        public TeachesController(ITeachesService teachesService)
        {
            _teachesService = teachesService;
        }

        // GET: api/Teaches
        public IQueryable<TeacherCourseUserModel> GetTeacherCourses()
        {
            return _teachesService.GetTeacherCourses();
        }

        // GET: api/Teaches/5
        [ResponseType(typeof(TeacherCourseUserModel))]
        public IHttpActionResult GetTeacherCourses(int id)
        {
            TeacherCourseUserModel teachObj = _teachesService.GetTeacherCourses(id);
            if (teachObj == null)
            {
                return NotFound();
            }
            return Ok(teachObj);
        }

        // PUT: api/Teaches/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacherCourses(int id, TeacherCourseUserModel teachModel)
        {
            bool result;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                result = _teachesService.UpdateTeacherCourses(id, teachModel);
            }
            catch (Exception)
            {
                if (!_teachesService.IsTeachesExists(id))
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

        [ResponseType(typeof(Teach))]
        public IHttpActionResult PostTeacherCourse(TeacherCourseUserModel teachModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = _teachesService.AddTeacherCourse(teachModel);
                if (result)
                    return CreatedAtRoute("DefaultApi", new { courseId = teachModel.CourseID,
                    teacherId = teachModel.TeacherID
                    }, teachModel);
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

        // DELETE: api/Teaches/5
        [ResponseType(typeof(Teach))]
        public IHttpActionResult DeleteTeacherCourse(int id)
        {
            try
            {
                TeacherCourseUserModel teach = _teachesService.DeleteTeacherCourse(id);
                if (teach != null)
                {
                    return Ok(teach);
                }
                else
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}