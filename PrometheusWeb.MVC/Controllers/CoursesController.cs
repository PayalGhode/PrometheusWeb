using Newtonsoft.Json;
using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.Exceptions;
using PrometheusWeb.MVC.Models.ViewModels;
using PrometheusWeb.Utilities;
using SendGrid.Helpers.Mail;
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
using System.Web.UI.WebControls;

namespace PrometheusWeb.MVC.Controllers
{
    public class CoursesController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44375/";
        
        // GET: Course
        
        public ActionResult Index()
        {
            ViewBag.Title = "Course Index Page";
            return View();
        }

        // POST: Course/AddCourses
        [Authorize(Roles = "admin")]
        public ActionResult AddCourse(int id = 0)
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
                        return View(new CourseUserModel());
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddCourse(CourseUserModel course)
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
                    if (course.CourseID == 0)
                    {
                        if (course.StartDate.HasValue)
                        {
                            TimeSpan diff = (DateTime)course.EndDate - (DateTime)course.StartDate;
                            if (diff.Days == 0)
                            {
                                TempData["ErrorMessage"] = "Course StartDate cannot be same with EndDate";
                                return View();
                            }
                        }
                        if (course.EndDate.HasValue)
                        {
                            TimeSpan diff = (DateTime)course.EndDate - (DateTime)course.StartDate;
                            if (diff.Days < 0)
                            {
                                TempData["ErrorMessage"] = "Course EndDate cannot be before StartDate";
                                return View();
                            }
                        }
                        HttpResponseMessage responseStudent = GlobalVariables.WebApiClient.PostAsJsonAsync("api/Courses/", course).Result;
                        if (responseStudent.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = "Course Added Successfully";
                            ViewBag.Message = "Course Added Successfully";
                        }
                        else if (responseStudent.StatusCode == HttpStatusCode.Conflict)
                        {
                            TempData["ErrorMessage"] = "Course already Added";
                            ViewBag.Message = "Course already Added";
                        }

                        else
                        {
                            TempData["ErrorMessage"] = "There was error registering a Course!";
                            ViewBag.Message = "There was error registering a Course!";
                        }
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
                return RedirectToAction("AddCourse");
            }
        }

        // POST: Courses/UpdateCourse
        [Authorize(Roles = "admin")]
        public ActionResult UpdateCourse(int id = 0)
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
                        HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Courses/" + id.ToString()).Result;
                        return View(response.Content.ReadAsAsync<CourseUserModel>().Result);
                    }
                    
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
                return RedirectToAction("ViewCourses");
            }   
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateCourse(CourseUserModel course)
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
                    if (course.CourseID != 0)
                    {
                        if (course.StartDate.HasValue)
                        {
                            TimeSpan diff = (DateTime)course.EndDate - (DateTime)course.StartDate;
                            if (diff.Days == 0)
                            {
                                TempData["ErrorMessage"] = "Course StartDate cannot be same with EndDate";
                                return View();
                            }
                        }
                        if (course.EndDate.HasValue)
                        {
                            TimeSpan diff = (DateTime)course.EndDate - (DateTime)course.StartDate;
                            if (diff.Days < 0)
                            {
                                TempData["ErrorMessage"] = "Course EndDate cannot be before StartDate";
                                return View();
                            }
                        }
                        HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("api/Courses/" + course.CourseID, course).Result;
                        TempData["SuccessMessage"] = "Course Updated Successfully";
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return RedirectToAction("ViewCourses");
            }
        }

        // DELETE: Courses/DeleteCourse
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCourse(int id)
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
                    HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("api/Courses/" + id.ToString()).Result;
                    TempData["SuccessMessage"] = "Course Deleted Successfully";
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
                return RedirectToAction("ViewCourses");
            }    
        }

        // GET: Courses/ViewCourses
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ViewCourses()
        {
            List<CourseUserModel> courses = new List<CourseUserModel>();

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
                    HttpResponseMessage ResFromCourses = await client.GetAsync("api/Courses/");


                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromCourses.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var courseResponse = ResFromCourses.Content.ReadAsStringAsync().Result;


                        //Deserializing the response recieved from web api and storing into the list  
                        courses = JsonConvert.DeserializeObject<List<CourseUserModel>>(courseResponse);

                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                //returning the employee list to view  
                return View(courses);
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> SearchCourse(string search)
        {
            List<CourseUserModel> courses = new List<CourseUserModel>();

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
                    //Sending request to find web api REST service resource Get:Students using HttpClient  
                    HttpResponseMessage ResFromCourses = await client.GetAsync("api/Courses/");


                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromCourses.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var courseResponse = ResFromCourses.Content.ReadAsStringAsync().Result;


                        //Deserializing the response recieved from web api and storing into the list  
                        courses = JsonConvert.DeserializeObject<List<CourseUserModel>>(courseResponse);

                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                //returning the employee list to view  
                return View(courses.Where(x => x.Name.ToLower().Contains(search.ToLower()) | search == null).ToList());
            }
        }

        // GET: Student/ViewCourses
        [Authorize(Roles ="student")]
        public async Task<ActionResult> ViewCoursesEnrollment(int id = 1)  //@TODO: change default to 0 after auth
        {
            List<CourseUserModel> courses = new List<CourseUserModel>();

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
                    HttpResponseMessage ResFromCourses = await client.GetAsync("api/Courses/");
                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromCourses.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var courseResponse = ResFromCourses.Content.ReadAsStringAsync().Result;


                        //Deserializing the response recieved from web api and storing into the list  
                        courses = JsonConvert.DeserializeObject<List<CourseUserModel>>(courseResponse);

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                //returning the employee list to view  
                return View(courses);
            }
        }

        // GET: Student/MyCourses
        [Authorize]
        public async Task<ActionResult> StudentCourses(int id = 0)  
        {
            List<CourseUserModel> courses = new List<CourseUserModel>();
            List<EnrollmentUserModel> enrollments = new List<EnrollmentUserModel>();

            var identity = (ClaimsIdentity)User.Identity;
            int studentID;
            if (id==0)
            {

            
            var ID = identity.Claims.Where(c => c.Type == "ID")
                        .Select(c => c.Value).FirstOrDefault();
            
            try
            {
                if(ID != null)
                {
                    studentID = Int32.Parse(ID);
                }
                else
                {
                    throw new PrometheusWebException("Failed to retrieve ID");
                }
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(500);
            }
            }
            else
            {
                studentID = id;
            }
            var token = identity.Claims.Where(c => c.Type == "AcessToken")
                        .Select(c => c.Value).FirstOrDefault();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get:Enrollemnts using HttpClient  
                HttpResponseMessage ResFromEnrollment = await client.GetAsync("api/Enrollments/");

                //Checking the response is successful or not which is sent using HttpClient  
                if (ResFromEnrollment.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    try
                    {
                        var enrollmentResponse = ResFromEnrollment.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the list  
                        enrollments = JsonConvert.DeserializeObject<List<EnrollmentUserModel>>(enrollmentResponse);

                        var result = enrollments.Where(item => item.StudentID == studentID).ToList();
                        if (result.Any())
                        {
                            return View(result);
                        }
                    }
                    catch
                    {
                        return new HttpStatusCodeResult(500);
                    }

                }
                //returning the employee list to view  
                return new HttpStatusCodeResult(404);
            }
        }

        // GET: Student/EnrollInCourse
        [Authorize(Roles ="student")]
        public async Task<ActionResult> EnrollInCourse(CourseUserModel course)  
        {
            if (course.StartDate.HasValue)
            {
                TimeSpan diff = DateTime.Now - (DateTime)course.StartDate;
                if (diff.Days > 7)
                {
                    TempData["ErrorMessage"] = "Registration for this course is over!";
                    ViewBag.Message = "Registration for this course is over!";
                    return View();
                }
            }
            if (course.EndDate.HasValue)
            {
                TimeSpan diff = DateTime.Now - (DateTime)course.EndDate;
                if (diff.Days > 0)
                {
                    TempData["ErrorMessage"] = "This Course Is Over. We Will Notify You when this course is back";
                    ViewBag.Message = "This Course Is Over. We Will Notify You when this course is back!";
                    return View();
                }
            }

            var identity = (ClaimsIdentity)User.Identity;
            var ID = identity.Claims.Where(c => c.Type == "ID")
                        .Select(c => c.Value).FirstOrDefault();
            int studentID;
            try
            {
                if (ID != null)
                {
                    studentID = Int32.Parse(ID);
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
            //TODO: Get Student ID from Auth

            EnrollmentUserModel enrollments = new EnrollmentUserModel
            {
                CourseID = course.CourseID,
                StudentID = studentID
            };

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get:Enrollemnts using HttpClient  
                HttpResponseMessage ResFromEnrollment = await client.PostAsJsonAsync("api/Enrollments/", enrollments);

                //Checking the response is successful or not which is sent using HttpClient  
                if (ResFromEnrollment.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Enrolled Successfully";
                    ViewBag.Message = "Enrolled Successfully!";

                }
                else if (ResFromEnrollment.StatusCode == HttpStatusCode.Conflict)
                {
                    TempData["ErrorMessage"] = "Already Enrolled!";
                    ViewBag.Message = "Already Enrolled!";
                }

                else
                {

                    TempData["SuccessMessage"] = "There was error enrolling in Course!";
                    ViewBag.Message = "There was error enrolling in Course!";
                }

                return View();
            }
        }

        // GET: Teacher/MyCourses
        [Authorize(Roles = "admin,teacher")]
        public async Task<ActionResult> TeacherCourses(int id = 0)  //@TODO: change default to 0 after auth
        {
            
            int TeacherId;
            //Take Identity
            var identity = (ClaimsIdentity)User.Identity;

            if (id == 0)
            {
                var ID = identity.Claims.Where(c => c.Type == "ID")
                            .Select(c => c.Value).FirstOrDefault();
                //validate the ID
                try
                {
                    if (ID != null)
                    {
                        TeacherId = Int32.Parse(ID);
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
            }
            else
            {
                TeacherId = id;
            }
            //create token
            var token = identity.Claims.Where(c => c.Type == "AcessToken")
                        .Select(c => c.Value).FirstOrDefault();

            //List of all courses
            List<CourseUserModel> courses = new List<CourseUserModel>();

            //list of teachercourses
            List<TeacherCourseUserModel> teachingCourses = new List<TeacherCourseUserModel>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get:Courses & Get:Teacher Courses using HttpClient  
                HttpResponseMessage ResFromCourses = await client.GetAsync("api/Courses/");
                HttpResponseMessage ResFromTeachingCourses = await client.GetAsync("api/Teaches/");

                //Checking the response is successful or not which is sent using HttpClient  
                if (ResFromCourses.IsSuccessStatusCode && ResFromTeachingCourses.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var courseResponse = ResFromCourses.Content.ReadAsStringAsync().Result;
                    var TeachingCourseResponse = ResFromTeachingCourses.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the list  
                    courses = JsonConvert.DeserializeObject<List<CourseUserModel>>(courseResponse);
                    teachingCourses = JsonConvert.DeserializeObject<List<TeacherCourseUserModel>>(TeachingCourseResponse);

                    try
                    {
                        var result = teachingCourses.Where(item => item.TeacherID == TeacherId).Join(
                        courses,
                        teachingCourse => teachingCourse.CourseID,
                        course => course.CourseID,
                        (teachingCourse, course) => new TeacherCourses
                        {
                            TeacherID = (int)teachingCourse.TeacherID,
                            CourseID = (int)teachingCourse.CourseID,
                            Name = course.Name,
                            StartDate = (DateTime)course.StartDate,
                            EndDate = (DateTime)course.EndDate
                        }
                        ).ToList();
                        if (result.Any())
                        {
                            return View(result);
                        }
                        if(result.Count == 0)
                        {
                            TempData["Message"] = "No Courses Selected For Teaching";
                            ViewBag.Message = "No Courses Selected For Teaching";
                        }
                    }
                    catch
                    {
                        return new HttpStatusCodeResult(500);
                    }

                }
                else
                {
                    throw new PrometheusWebException("No Course Selected For Teaching");
                    /*TempData["Message"] = "No Courses Selected For Teaching";
                    ViewBag.Message = "No Courses Selected For Teaching";*/
                }
                //returning the StatusCode 
                return new HttpStatusCodeResult(404);
            }
            

        }

        // GET: Teacher/ViewCourses
        [Authorize(Roles = "admin, teacher")]
        public async Task<ActionResult> ViewCoursesForTeaching()  
        {
            List<CourseUserModel> courses = new List<CourseUserModel>();

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
                    //Sending request to find web api REST service resource Get:Courses using HttpClient  
                    HttpResponseMessage ResFromCourses = await client.GetAsync("api/Courses/");
                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromCourses.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var courseResponse = ResFromCourses.Content.ReadAsStringAsync().Result;


                        //Deserializing the response recieved from web api and storing into the list  
                        courses = JsonConvert.DeserializeObject<List<CourseUserModel>>(courseResponse);
                        
                    }
                    else
                    {
                        throw new PrometheusWebException("Courses Not Avaialable!");
                        /*TempData["ErrorMessage"] = "No Courses  Available";
                        ViewBag.Message = "No Courses  Available!";*/
                    }
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(500);
                }
                return View(courses);
            }
        }

       [ Authorize(Roles = "admin,teacher")]
        //GET: Teacher/Save
        public async Task<ActionResult> SaveCourses(int courseId, int id = 0)
        {
            //select identity
            var identity = (ClaimsIdentity)User.Identity;
            var ID = identity.Claims.Where(c => c.Type == "ID")
                        .Select(c => c.Value).FirstOrDefault();
            int teacherID;

            //validate Teacher ID
            try
            {
                if (ID != null)
                {
                    teacherID = Int32.Parse(ID);
                }
                else
                {
                    //throw exception when failed to retrieeve id
                    throw new PrometheusWebException("Failed to retrieve ID");
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(500);
            }
            var token = identity.Claims.Where(c => c.Type == "AcessToken")
                        .Select(c => c.Value).FirstOrDefault();

           

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    //Sending request to find web api REST service resource Get:Courses & Get:Enrollemnts using HttpClient  
                    HttpResponseMessage ResFromCourses = await client.GetAsync("api/Courses/" + courseId.ToString());
                    HttpResponseMessage ResFromTeaches = await client.GetAsync("api/Teaches/" + teacherID.ToString());

                    //Storing the response details recieved from web api   
                    var teacherCourseResponse = ResFromCourses.Content.ReadAsAsync<CourseUserModel>().Result;
                    return View(teacherCourseResponse);
                }
                catch(Exception)
                {
                    //returning the StatusCode 
                    return new HttpStatusCodeResult(500);
                }
               
            }
        }

        [Authorize(Roles ="admin,teacher")]
        //POST : Teacher/SaveCourses
        [HttpPost]
        public async Task<ActionResult> SaveCourses(CourseUserModel courseModel)
        {
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

            if (courseModel.StartDate.HasValue)
            {
                TimeSpan diff = DateTime.Now - (DateTime)courseModel.StartDate;
                if (diff.Days > 7)
                {
                    TempData["ErrorMessage"] = "Course Cannot be Selected!";
                    return View();
                }
            }
            TeacherCourseUserModel teaches = new TeacherCourseUserModel
            {
                CourseID = courseModel.CourseID,
                TeacherID = teacherID
            };
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to Post web api REST service resource using HttpClient  
                HttpResponseMessage ResFromTeaches = await client.PostAsJsonAsync("api/Teaches/", teaches);
                try
                {
                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromTeaches.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Courses Selected Successfully For Teaching";
                        ViewBag.Message = "Courses Selected Successfully For Teaching";
                    }
                    else if (ResFromTeaches.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["ErrorMessage"] = "Already Selected!";
                        ViewBag.Message = "Already Selected";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "There was error Selecting Course for Teaching!";
                        ViewBag.Message = "There was error Selecting Course for Teaching!";
                    }
                }
                catch(Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
                
            }
            return RedirectToAction("TeacherCourses");
        }
    }
}