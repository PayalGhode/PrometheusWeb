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
    public class CoursesController : ApiController
    {
        private ICourseService _courseService = null;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/Courses
        [Authorize]
        public IQueryable<CourseUserModel> GetCourses()
        {
            return _courseService.GetCourses();
        }

        // GET: api/Courses/5
        [ResponseType(typeof(CourseUserModel))]
        public IHttpActionResult GetCourse(int id)
        {
            CourseUserModel course = _courseService.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourse(int id, CourseUserModel courseModel)
        {
            bool result;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                result = _courseService.UpdateCourse(id, courseModel);
            }
            catch (Exception)
            {
                if (!_courseService.IsCourseExists(id))
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

        // POST: api/Courses
        [ResponseType(typeof(Course))]
        public IHttpActionResult PostCourse(CourseUserModel courseModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = _courseService.AddCourse(courseModel);
                if (result)
                    return CreatedAtRoute("DefaultApi", new { id = courseModel.Name }, courseModel);
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

        // DELETE: api/Courses/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult DeleteCourse(int id)
        {
            CourseUserModel course = _courseService.DeleteCourse(id);

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }

    }
}