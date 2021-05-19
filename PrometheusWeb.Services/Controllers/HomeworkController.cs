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
using PrometheusWeb.Exceptions;
using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.Services.Services;

namespace PrometheusWeb.Services.Controllers
{
    public class HomeworkController : ApiController
    {
        private IHomeworkService _homeworkService = null;
        public HomeworkController(IHomeworkService homeworkService)
        {
            _homeworkService = homeworkService;
        }

        //Get All Homeworks
        // GET: api/Homework
        public IQueryable<HomeworkUserModel> GetHomeworks()
        {
            try
            {
                IQueryable<HomeworkUserModel> homeworks = _homeworkService.GetHomeworks();
                return homeworks;
            }
            catch(Exception)
            {
                return null;
            }
        }


        //Get  Homeworks By ID
        // GET: api/Homework/5
        [ResponseType(typeof(HomeworkUserModel))]
        public IHttpActionResult GetHomework(int id)
        {
            HomeworkUserModel homework = _homeworkService.GetHomework(id);
            if (homework == null)
            {
                return NotFound();
            }
            return Ok(homework);
        }

        // PUT: api/Homework/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHomework(int id, HomeworkUserModel homeworkModel)
        {
            bool result;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                result = _homeworkService.UpdateHomework(id, homeworkModel);
            }
            catch (Exception)
            {
                if (!_homeworkService.IsHomeworkExists(id))
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

        // POST: api/Homework
        [ResponseType(typeof(Homework))]
        public IHttpActionResult PostHomework(HomeworkUserModel homeworkModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = _homeworkService.AddHomework(homeworkModel);
                if (result)
                    return CreatedAtRoute("DefaultApi", new { id = homeworkModel.HomeWorkID }, homeworkModel);
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

        // DELETE: api/Homework/5
        [ResponseType(typeof(Homework))]
        public IHttpActionResult DeleteHomework(int id)
        {
            try
            {
                HomeworkUserModel homework = _homeworkService.DeleteHomework(id);
                if(homework != null)
                {
                    return Ok(homework);
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