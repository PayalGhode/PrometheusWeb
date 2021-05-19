using Newtonsoft.Json;
using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.MVC.Models.ViewModels;
using PrometheusWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using PrometheusWeb.Exceptions;
using System.Web.Mvc;

namespace PrometheusWeb.MVC.Controllers
{
    public class StudentController : Controller
    {
        //Hosted web API REST Service base url  
        const string Baseurl = "https://localhost:44375/";
        // GET: Student

        public ActionResult Index()
        {
            ViewBag.Title = "Student Index Page";
            return View();
        }

        // POST: Student/AddStudent
        [Authorize(Roles = "admin")]
        public ActionResult AddStudent(int id = 0)
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

                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddStudent(AdminUserModel user)
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
                    if (user.StudentID == 0)
                    {
                        user.Role = "student";
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

                        //Sending request to find web api REST service resource Post:Users using HttpClient
                        HttpResponseMessage responseUser = GlobalVariables.WebApiClient.PostAsJsonAsync("api/Users/", user).Result;

                        if (responseUser.IsSuccessStatusCode)
                        {
                            //Sending request to find web api REST service resource Post: using HttpClient
                            HttpResponseMessage responseStudent = GlobalVariables.WebApiClient.PostAsJsonAsync("api/Students/", user).Result;

                            if (responseStudent.IsSuccessStatusCode)
                            {
                                TempData["SuccessMessage"] = "Student Added Successfully";
                                ViewBag.Message = "Student Added Successfully";

                                TempData["SuccessMessage"] = "Student Added Successfully";
                                ViewBag.Message = "Student Added Successfully";

                            }
                            else if (responseStudent.StatusCode == HttpStatusCode.Conflict)
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
                            TempData["ErrorMessage"] = "There was error registering a Student!";
                            ViewBag.Message = "There was error registering a Student!";
                        }
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return RedirectToAction("AddStudent");
            }
        }

        // GET: Student/ViewStudents
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ViewStudents()
        {
            List<StudentUserModel> students = new List<StudentUserModel>();

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
                    HttpResponseMessage ResFromCourses = await client.GetAsync("api/Students/");


                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromCourses.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var studentResponse = ResFromCourses.Content.ReadAsStringAsync().Result;


                        //Deserializing the response recieved from web api and storing into the list  
                        students = JsonConvert.DeserializeObject<List<StudentUserModel>>(studentResponse);
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                //returning the employee list to view  
                return View(students);
            }
        }

        // DELETE: Student/DeleteStudent
        [Authorize(Roles = "admin")]
        public ActionResult DeleteStudent(int id)
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
                    HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("api/Students/" + id.ToString()).Result;
                    TempData["SuccessMessage"] = "Student Deleted Successfully";
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return RedirectToAction("ViewStudents");
            }
        }

        // POST: Student/UpdateStudent
        [Authorize(Roles = "admin")]
        public ActionResult UpdateStudent(int id = 0)
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
                        HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Students/" + id.ToString()).Result;
                        return View(response.Content.ReadAsAsync<StudentUserModel>().Result);
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return RedirectToAction("ViewStudents");
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateStudent(StudentUserModel student)
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
                    if (student.StudentID != 0)
                    {
                        if (student.DOB.HasValue)
                        {
                            TimeSpan diff = DateTime.Now - (DateTime)student.DOB;
                            if (diff.Days == 0)
                            {
                                TempData["ErrorMessage"] = "DOB cannot be same with CurrentDate";
                                ViewBag.Message = "DOB cannot be same with CurrentDate";
                                return View();
                            }
                            if (student.DOB > DateTime.Now)
                            {
                                TempData["ErrorMessage"] = "DOB cannot be CurrentDate or after CurrentDate";
                                ViewBag.Message = "DOB cannot be same with CurrentDate";
                                return View();
                            }
                        }
                        HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("api/Students/" + student.StudentID, student).Result;
                        TempData["SuccessMessage"] = "Student Updated Successfully";
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
                return RedirectToAction("ViewStudents");
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> SearchStudent(string search)
        {
            List<StudentUserModel> students = new List<StudentUserModel>();

            using (var client = new HttpClient())
            {
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
                    HttpResponseMessage ResFromCourses = await client.GetAsync("api/Students/");


                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromCourses.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var studentResponse = ResFromCourses.Content.ReadAsStringAsync().Result;


                        //Deserializing the response recieved from web api and storing into the list  
                        students = JsonConvert.DeserializeObject<List<StudentUserModel>>(studentResponse);

                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
                
                //returning the employee list to view  
                return View(students.Where(x => x.FName.ToLower().Contains(search.ToLower()) | search == null).ToList());
            }
        }

        //View My Students
        [Authorize(Roles = "admin,teacher")]
        public async Task<ActionResult> EnrolledStudents(int courseId)  //@TODO: change default to 0 after auth
        {
            List<StudentUserModel> students = new List<StudentUserModel>();
            List<EnrollmentUserModel> enrollments = new List<EnrollmentUserModel>();
            List<CourseUserModel> courses = new List<CourseUserModel>();

            using (var client = new HttpClient())
            {
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
                    //Sending request to find web api REST service resource using HttpClient  
                    HttpResponseMessage ResFromStudents = await client.GetAsync("api/Students/");
                    HttpResponseMessage ResFromEnrollment = await client.GetAsync("api/Enrollments/");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromStudents.IsSuccessStatusCode && ResFromEnrollment.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var studentResponse = ResFromStudents.Content.ReadAsStringAsync().Result;
                        var enrollmentResponse = ResFromEnrollment.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the list  
                        students = JsonConvert.DeserializeObject<List<StudentUserModel>>(studentResponse);
                        enrollments = JsonConvert.DeserializeObject<List<EnrollmentUserModel>>(enrollmentResponse);

                        try
                        {
                            var result = enrollments.Where(item => item.CourseID == courseId).Join(
                            students,
                            enrollment => enrollment.StudentID,
                            student => student.StudentID,
                            (enrollment, student) => new StudentUserModel
                            {
                                StudentID = student.StudentID,
                                FName = student.FName,
                                LName = student.LName,
                                MobileNo = student.MobileNo,
                                Address = student.Address,
                                City = student.City,
                                DOB = student.DOB
                            }
                            ).ToList();

                            if (result.Any())
                            {
                                return View(result);
                            }
                            else
                            {
                                throw new PrometheusWebException("No Students Enrolled in this Course");
                                ViewBag.Message = "No Students Enrolled in this Course";
                                return ViewBag.Message;
                            }
                        }
                        catch
                        {
                            return new HttpStatusCodeResult(500);
                        }
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                //returning the employee list to view  
                return new HttpStatusCodeResult(404);
            }
        }

        [HttpGet]
        [Authorize(Roles = "student")]
        public ActionResult UpdateStudentProfile(int id = 1)
        {
            using (var client = new HttpClient())
            {
                //Getting Required Data from Identity(App Cookie)
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

                        HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("api/Students/" + studentID.ToString()).Result;
                        return View(response.Content.ReadAsAsync<StudentUserModel>().Result);
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
        [Authorize(Roles = "student")]
        public ActionResult UpdateStudentProfile(StudentUserModel student)
        {
            using (var client = new HttpClient())
            {
                //Getting Required Data from Identity(App Cookie)
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
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);


                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    if (student.StudentID != 0)
                    {
                        if (student.DOB.HasValue)
                        {
                            TimeSpan diff = DateTime.Now - (DateTime)student.DOB;
                            if (diff.Days == 0)
                            {
                                TempData["ErrorMessage"] = "DOB cannot be same with CurrentDate";
                                ViewBag.Message = "DOB cannot be same with CurrentDate";
                                return View();
                            }
                            if (student.DOB > DateTime.Now)
                            {
                                TempData["ErrorMessage"] = "DOB cannot be CurrentDate or after CurrentDate";
                                ViewBag.Message = "DOB cannot be same with CurrentDate";
                                return View();
                            }
                        }
                        //Sending request to Post web api REST service resource using WebAPIClient and getting the result  
                        HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("api/Students/" + student.StudentID, student).Result;
                        TempData["SuccessMessage"] = "Profile Updated Successfully";
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return RedirectToAction("UpdateStudentProfile");
            }
        }

    }
}
