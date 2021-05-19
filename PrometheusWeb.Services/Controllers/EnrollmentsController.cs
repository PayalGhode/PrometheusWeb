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
    public class EnrollmentsController : ApiController
    {
        private IEnrollmentService _enrollmentService = null;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        // GET: api/Enrollments
        public IQueryable<EnrollmentUserModel> GetEnrollments()
        {
            try
            {
                IQueryable<EnrollmentUserModel> enrollments = _enrollmentService.GetEnrollments();
                return enrollments;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: api/Enrollments/5
        [ResponseType(typeof(Enrollment))]
        public IHttpActionResult GetEnrollment(int id)
        {
            EnrollmentUserModel enrollment = _enrollmentService.GetEnrollment(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return Ok(enrollment);
        }

        // PUT: api/Enrollments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEnrollment(int id, EnrollmentUserModel enrollment)
        {
            bool result;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                result = _enrollmentService.UpdateEnrollment(id, enrollment);
            }
            catch (Exception)
            {
                if (!_enrollmentService.IsEnrollmentExists(id))
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

        // POST: api/Enrollments
        [ResponseType(typeof(Enrollment))]
        public IHttpActionResult PostEnrollment(EnrollmentUserModel enrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = _enrollmentService.AddEnrollment(enrollment);
                if (result)
                    return CreatedAtRoute("DefaultApi", new { id = enrollment.EnrollmentID }, enrollment);
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

        // DELETE: api/Enrollments/5
        [ResponseType(typeof(Enrollment))]
        public IHttpActionResult DeleteEnrollment(int id)
        {
            try
            {
                EnrollmentUserModel enrollment = _enrollmentService.DeleteEnrollment(id);
                if (enrollment != null)
                {
                    return Ok(enrollment);
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