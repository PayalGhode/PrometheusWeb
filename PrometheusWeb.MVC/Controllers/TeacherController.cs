using Newtonsoft.Json;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PrometheusWeb.Exceptions;


namespace PrometheusWeb.MVC.Controllers
{
    public class TeacherController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44375/";

        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/ViewTeachers
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ViewTeachers()
        {
            List<TeacherUserModel> teachers = new List<TeacherUserModel>();

            using (var client = new HttpClient())
            {
                //Getting Required Data from Identity(App Cookie)
                var identity = (ClaimsIdentity)User.Identity;

                var token = identity.Claims.Where(c => c.Type == "AcessToken")
                            .Select(c => c.Value).FirstOrDefault();
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);


                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    //Sending request to find web api REST service resource Get:Courses & Get:Enrollemnts using HttpClient  
                    HttpResponseMessage ResFromCourses = await client.GetAsync("api/Teachers/");


                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromCourses.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var teacherResponse = ResFromCourses.Content.ReadAsStringAsync().Result;


                        //Deserializing the response recieved from web api and storing into the list  
                        teachers = JsonConvert.DeserializeObject<List<TeacherUserModel>>(teacherResponse);

                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
                
                //returning the employee list to view  
                return View(teachers);
            }
        }

        // POST: Teacher/AddTeacher
        [Authorize(Roles = "admin")]
        public ActionResult AddTeacher(int id = 0)
        {
            using (var client = new HttpClient())
            {
                //Getting Required Data from Identity(App Cookie)
                var identity = (ClaimsIdentity)User.Identity;

                var token = identity.Claims.Where(c => c.Type == "AcessToken")
                            .Select(c => c.Value).FirstOrDefault();
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);


                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    if (id == 0)
                    {
                        var list = new List<string>() { "What is your Pet Name?", "What is your Nick Name", "What is your School Name?" };
                        ViewBag.list = list;
                        return View(new AdminUserModel());
                    }
                }
                catch (Exception)
                {

                    return new HttpStatusCodeResult(500);
                }
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddTeacher(AdminUserModel user)
        {
            using (var client = new HttpClient())
            {
                //Getting Required Data from Identity(App Cookie)
                var identity = (ClaimsIdentity)User.Identity;

                var token = identity.Claims.Where(c => c.Type == "AcessToken")
                            .Select(c => c.Value).FirstOrDefault();
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);


                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    if (user.TeacherID == 0)
                    {
                        if (user.IsAdmin == true)
                        {
                            user.Role = "admin";
                        }
                        else
                        {
                            user.Role = "teacher";
                        }
                        var list = new List<string>() { "What is your Pet Name?", "What is your Nick Name", "What is your School Name?" };
                        ViewBag.list = list;
                        if (user.DOB.HasValue)
                        {
                            TimeSpan diff = DateTime.Now - (DateTime)user.DOB;
                            if (diff.Days == 0)
                            {
                                TempData["ErrorMessage"] = "DOB cannot be same with CurrentDate";
                                ViewBag.Message = "DOB cannot be same with CurrentDate";
                                return View();
                            }
                            if (user.DOB > DateTime.Now)
                            {
                                TempData["ErrorMessage"] = "DOB cannot be CurrentDate or after CurrentDate";
                                ViewBag.Message = "DOB cannot be same with CurrentDate";
                                return View();
                            }
                        }
                        HttpResponseMessage responseUser = GlobalVariables.WebApiClient.PostAsJsonAsync("api/Users/", user).Result;

                        if (responseUser.IsSuccessStatusCode)
                        {
                            HttpResponseMessage responseTeacher = GlobalVariables.WebApiClient.PostAsJsonAsync("api/Teachers/", user).Result;

                            if (responseTeacher.IsSuccessStatusCode)
                            {
                                TempData["SuccessMessage"] = "Teacher Added Successfully";
                                ViewBag.Message = "Teacher Added Successfully";
                            }
                            else if (responseTeacher.StatusCode == HttpStatusCode.Conflict)
                            {
                                TempData["ErrorMessage"] = "Phone No Already Taken try another Phone No";
                                ViewBag.Message = "Phone No Already Taken try another Phone No";
                            }

                            else
                            {
                                TempData["ErrorMessage"] = "There was error registering a Teacher!";
                                ViewBag.Message = "There was error registering a Teacher!";
                            }

                        }
                        else if (responseUser.StatusCode == HttpStatusCode.Conflict)
                        {
                            TempData["ErrorMessage"] = "UserID Already Taken";
                            ViewBag.Message = "UserID Already Taken";
                        }

                        else
                        {
                            TempData["ErrorMessage"] = "There was error registering a Teacher!";
                            ViewBag.Message = "There was error registering a Teacher!";
                        }
                    }
                }
                catch (Exception)
                {

                    return new HttpStatusCodeResult(500);
                }

                return RedirectToAction("AddTeacher");
            }

        }

        // DELETE: Teacher/DeleteTeacher
        [Authorize(Roles = "admin")]
        public ActionResult DeleteTeacher(int id)
        {
            using (var client = new HttpClient())
            {
                //Getting Required Data from Identity(App Cookie)
                var identity = (ClaimsIdentity)User.Identity;

                var token = identity.Claims.Where(c => c.Type == "AcessToken")
                            .Select(c => c.Value).FirstOrDefault();
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);


                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("api/Teachers/" + id.ToString()).Result;
                    HttpResponseMessage responseUser = GlobalVariables.WebApiClient.DeleteAsync("api/Users/" + id.ToString()).Result;
                    TempData["SuccessMessage"] = "Teacher Deleted Successfully";
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return RedirectToAction("ViewTeachers");
            }
        }

        // POST: Teacher/UpdateTeacher
        [Authorize(Roles = "admin")]
        public ActionResult UpdateTeacher(int id = 0)
        {
            using (var client = new HttpClient())
            {
                //Getting Required Data from Identity(App Cookie)
                var identity = (ClaimsIdentity)User.Identity;

                var token = identity.Claims.Where(c => c.Type == "AcessToken")
                            .Select(c => c.Value).FirstOrDefault();
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);


                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    if (id != 0)
                    {
                        HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Teachers/" + id.ToString()).Result;
                        return View(response.Content.ReadAsAsync<TeacherUserModel>().Result);
                    }
                }
                catch (Exception)
                {

                    return new HttpStatusCodeResult(500);
                }
                return RedirectToAction("ViewTeachers");
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateTeacher(TeacherUserModel teacher)
        {
            using (var client = new HttpClient())
            {
                //Getting Required Data from Identity(App Cookie)
                var identity = (ClaimsIdentity)User.Identity;

                var token = identity.Claims.Where(c => c.Type == "AcessToken")
                            .Select(c => c.Value).FirstOrDefault();
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);


                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    if (teacher.TeacherID != 0)
                    {
                        if (teacher.DOB.HasValue)
                        {
                            TimeSpan diff = DateTime.Now - (DateTime)teacher.DOB;
                            if (diff.Days == 0)
                            {
                                TempData["ErrorMessage"] = "DOB cannot be same with CurrentDate";
                                ViewBag.Message = "DOB cannot be same with CurrentDate";
                                return View();
                            }
                            if (teacher.DOB > DateTime.Now)
                            {
                                TempData["ErrorMessage"] = "DOB cannot be CurrentDate or after CurrentDate";
                                ViewBag.Message = "DOB cannot be same with CurrentDate";
                                return View();
                            }
                        }
                        HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("api/Teachers/" + teacher.TeacherID, teacher).Result;
                        TempData["SuccessMessage"] = "Teacher Updated Successfully";
                    }
                }
                catch (Exception)
                {

                    return new HttpStatusCodeResult(500);
                }
                
            }
            return RedirectToAction("ViewTeachers");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> SearchTeacher(string search)
        {
            List<TeacherUserModel> teachers = new List<TeacherUserModel>();

            using (var client = new HttpClient())
            {
                //Getting Required Data from Identity(App Cookie)
                var identity = (ClaimsIdentity)User.Identity;

                var token = identity.Claims.Where(c => c.Type == "AcessToken")
                            .Select(c => c.Value).FirstOrDefault();
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);


                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    //Sending request to find web api REST service resource  
                    HttpResponseMessage ResFromCourses = await client.GetAsync("api/Teachers/");


                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromCourses.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var teacherResponse = ResFromCourses.Content.ReadAsStringAsync().Result;


                        //Deserializing the response recieved from web api and storing into the list  
                        teachers = JsonConvert.DeserializeObject<List<TeacherUserModel>>(teacherResponse);

                    }
                }
                catch (Exception)
                {

                    return new HttpStatusCodeResult(500);
                }

                //returning the employee list to view  
                return View(teachers.Where(x => x.FName.ToLower().Contains(search.ToLower()) | search == null).ToList());
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin,teacher")]
        public ActionResult UpdateTeacherProfile(int id = 1)
        {
            using (var client = new HttpClient())
            {
                //Getting Required Data from Identity(App Cookie)
                var identity = (ClaimsIdentity)User.Identity;
                var ID = identity.Claims.Where(c => c.Type == "ID")
                            .Select(c => c.Value).FirstOrDefault();
                int teacherID;
                try
                {
                    if (ID != null)
                    {
                        teacherID = Int32.Parse(ID);
                    }
                    else
                    {
                        throw new PrometheusWebException("Failed to retrieve ID");
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
                var token = identity.Claims.Where(c => c.Type == "AcessToken")
                            .Select(c => c.Value).FirstOrDefault();
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);


                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    if (id != 0)
                    {

                        HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Teachers/" + teacherID.ToString()).Result;
                        return View(response.Content.ReadAsAsync<TeacherUserModel>().Result);
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [Authorize(Roles = "admin,teacher")]
        public ActionResult UpdateTeacherProfile(TeacherUserModel teacher)
        {
            using (var client = new HttpClient())
            {
                //Getting Required Data from Identity(App Cookie)
                var identity = (ClaimsIdentity)User.Identity;
                var ID = identity.Claims.Where(c => c.Type == "ID")
                            .Select(c => c.Value).FirstOrDefault();
                int teacherID;
                try
                {
                    if (ID != null)
                    {
                        teacherID = Int32.Parse(ID);
                    }
                    else
                    {
                        throw new PrometheusWebException("Failed to retrieve ID");
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
                var token = identity.Claims.Where(c => c.Type == "AcessToken")
                            .Select(c => c.Value).FirstOrDefault();
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);


                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    if (teacher.TeacherID != 0)
                    {
                        if (teacher.DOB.HasValue)
                        {
                            TimeSpan diff = DateTime.Now - (DateTime)teacher.DOB;
                            if (diff.Days == 0)
                            {
                                TempData["ErrorMessage"] = "DOB cannot be same with CurrentDate";
                                ViewBag.Message = "DOB cannot be same with CurrentDate";
                                return View();
                            }
                            if (teacher.DOB > DateTime.Now)
                            {
                                TempData["ErrorMessage"] = "DOB cannot be CurrentDate or after CurrentDate";
                                ViewBag.Message = "DOB cannot be same with CurrentDate";
                                return View();
                            }
                        }
                        //Sending request to Post web api REST service resource using WebAPIClient and getting the result  
                        HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("api/Teachers/" + teacher.TeacherID, teacher).Result;
                        TempData["SuccessMessage"] = "Profile Updated Successfully";
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return RedirectToAction("UpdateTeacherProfile");
            }
            
            
        }


        /*public ActionResult UpdateTeacherProfile(int id = 1)
        {
            if (id != 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Teachers/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<TeacherUserModel>().Result);
            }
            return RedirectToAction("Index");
        }*/

        /*[HttpPost]
        public ActionResult UpdateTeacherProfile(TeacherUserModel teacher)
        {
            if (teacher.TeacherID != 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("api/Teachers/" + teacher.TeacherID, teacher).Result;
                TempData["SuccessMessage"] = "Profile Updated Successfully";
            }
            return RedirectToAction("UpdateTeacherProfile");
        }*/
    }
}