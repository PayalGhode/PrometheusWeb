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
    public class UsersController : ApiController
    {
        private IUserService _userService = null;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Courses
        public IQueryable<AdminUserModel> GetUsers()
        {
            return _userService.GetUsers();
        }

        // GET: api/Courses/5
        [ResponseType(typeof(AdminUserModel))]
        public IHttpActionResult GetUser(string id)
        {
            AdminUserModel user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, AdminUserModel userModel)
        {
            bool result;
            
            try
            {
                result = _userService.UpdateUser(id, userModel);
            }
            catch (Exception)
            {
                if (!_userService.IsUserExists(id))
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
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(AdminUserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = _userService.AddUser(userModel);
                if (result)
                    return CreatedAtRoute("DefaultApi", new { id = userModel.UserID }, userModel);
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
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            AdminUserModel user = _userService.DeleteUser(id);

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}