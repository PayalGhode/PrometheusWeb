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
using PrometheusWeb.Services.Services;

namespace PrometheusWeb.Services.Controllers
{
    public class HomeworkPlansController : ApiController
    {
        private IHomeworkPlanService _homeworkPlanService;

        public HomeworkPlansController(IHomeworkPlanService homeworkPlanService)
        {
            _homeworkPlanService = homeworkPlanService;
        }

        // GET: api/HomeworkPlans
        public IQueryable<HomeworkPlanUserModel> GetHomeworkPlans()
        {
            return _homeworkPlanService.GetHomeworkPlans();
        }

        // GET: api/HomeworkPlansByStudentID
        [Route("api/HomeworkPlansByStudentID/{StudentID}")]
        public IQueryable<HomeworkPlanUserModel> GetHomeworkPlans(int StudentID)
        {
            return _homeworkPlanService.GetHomeworkPlans(StudentID);
        }



        // GET: api/HomeworkPlans/5
        [ResponseType(typeof(HomeworkPlanUserModel))]
        public IHttpActionResult GetHomeworkPlan(int id)
        {
            HomeworkPlanUserModel homeworkPlan = _homeworkPlanService.GetHomeworkPlan(id);
            if (homeworkPlan == null)
            {
                return NotFound();
            }

            return Ok(homeworkPlan);
        }

        // PUT: api/HomeworkPlans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHomeworkPlan(int id, HomeworkPlanUserModel homeworkPlan)
        {
            bool result;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                result = _homeworkPlanService.UpdateHomeworkPlan(id, homeworkPlan);
            }
            catch (Exception)
            {
                if (!_homeworkPlanService.IsHomeworkPlanExists(id))
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

        // POST: api/HomeworkPlans
     
        [ResponseType(typeof(HomeworkPlan))]
        public IHttpActionResult PostHomeworkPlan(HomeworkPlanUserModel homeworkPlan)
        {
            var result = _homeworkPlanService.AddHomeworkPlan(homeworkPlan);
            if (result)
                return CreatedAtRoute("DefaultApi", new { id = homeworkPlan.HomeworkPlanID }, homeworkPlan);
            else
                return StatusCode(HttpStatusCode.InternalServerError);
        }

        // POST: api/HomeworkPlans/Many
        [HttpPost]
        [Route("api/HomeworkPlans/Many")]
        [ResponseType(typeof(HomeworkPlan))]
        public IHttpActionResult PostHomeworkPlans(List<HomeworkPlanUserModel> homeworkPlans)
        {
            var result = _homeworkPlanService.AddHomeworkPlans(homeworkPlans.AsQueryable());
            if (result)
                return Ok(result);
            else
                return StatusCode(HttpStatusCode.InternalServerError);
        }

        // DELETE: api/HomeworkPlans/5
        [ResponseType(typeof(HomeworkPlan))]
        public IHttpActionResult DeleteHomeworkPlan(int id)
        {
            HomeworkPlanUserModel homeworkPlan = _homeworkPlanService.DeleteHomeworkPlan(id);

            return Ok(homeworkPlan);
        }

        // DELETE: api/HomeworkPlansByStudentID/5
        [ResponseType(typeof(bool))]
        public IHttpActionResult DeleteHomeworkPlans(int StudentID)
        {
            var result = _homeworkPlanService.DeleteHomeworkPlans(StudentID);

            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {       
            base.Dispose(disposing);
        }

    }
}