using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.MVC.Controllers;
using PrometheusWeb.MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace PrometheusWeb.MVC.Controllers.Tests
{

    [TestClass()]
    public class StudentControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            var controller = new StudentController();

            //Act
            var result = controller.Index();
            //Assert
            Assert.IsNotNull(result); //Testing for Null result
            Assert.IsInstanceOfType(result, typeof(ViewResult)); // Type Checking
                                                                 // Testing if VIewbag title is same
        }
        [TestMethod()]
        public void IndexReturnTypeTest()
        {
            //Arrange
            var controller = new StudentController();

            //Act
            var result = controller.Index();
            //Assert
           
            Assert.IsInstanceOfType(result, typeof(ViewResult)); // Type Checking
            
        }
        
        [TestMethod()]
        public void AddStudentTest()
        {
            //Arrange
            var controller = new StudentController();

            //Act
            var result = controller.AddStudent();
            //Assert
            Assert.IsNotNull(result); //Testing for Null result
            Assert.IsInstanceOfType(result, typeof(ViewResult)); // Type Checking
           
        }
        [TestMethod()]
        public void AddStudentTest1()
        {
            //Arrange
            var controller = new StudentController();
            AdminUserModel adminUserModel = new AdminUserModel
            {
                StudentID = 4,
                FName = "TestName",
                LName = "TestLastName",
                UserID = "TestUserID",
                DOB = DateTime.Now,
                Address = "TestAddress",
                City = "TestCity",
                MobileNo = "TestNumber"

            };

            //Act
            var result = controller.AddStudent(adminUserModel);
            //Assert
            Assert.IsNotNull(result); //Testing for Null result
          

        }
        [TestMethod()]
        public void ViewStudentTest()
        {
            //Arrange
            var controller = new StudentController();

            //Act
            var result = controller.ViewStudents().Result as  ViewResult;
            //Assert
            Assert.IsNotNull(result);
          
        }

        [TestMethod()]
        public void DeleteStudentTest()
        {
            //Arrange
            var controller = new StudentController();
             int id = 4;
            //Act
            var result = controller.DeleteStudent(id);
            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod()]
        public void UpdateStudentTest()
        {
            //Arrange
            var controller = new StudentController();
            
            //Act
            var result = controller.UpdateStudent();
            //Assert
            Assert.IsNotNull(result);

        }
        [TestMethod()]
        public void UpdateStudentTest1()
        {
            //Arrange
            var controller = new StudentController();
            StudentUserModel adminUserModel = new StudentUserModel
            {
                StudentID = 4,
                FName = "TestName",
                LName = "TestLastName",
                UserID = "TestUserID",
                DOB = DateTime.Now,
                Address = "TestAddress",
                City = "TestCity",
                MobileNo = "TestNumber"

            };

            //Act
            var result = controller.UpdateStudent(adminUserModel);
            //Assert
            Assert.IsNotNull(result); //Testing for Null result


        }
        [TestMethod()]
        public void SearchStudentTest()
        {
            //Arrange
            var controller = new StudentController();
            string test = "TestString";
            //Act
            var result = controller.SearchStudent(test);
            //Assert
            Assert.IsNotNull(result);

        }

        //[TestMethod()]
        //public void EnrolledStudentTest()
        //{
        //    //Arrange
        //    var controller = new StudentController();
          
        //    //Act
        //    var result = controller.EnrolledStudents();
        //    //Assert
        //    Assert.IsNotNull(result);

        //}

       
        /*
        [TestMethod()]
        public void ViewCoursesTest()
        {
            //Arrange
            var controller = new StudentController();

            //Act
            var result = controller.ViewCourses().Result as ViewResult;
            //Assert
            Assert.IsNotNull(result); //Testing for Null result
            Assert.IsInstanceOfType(result, typeof(ViewResult)); // Type Checking
            Assert.IsInstanceOfType(result.Model, typeof(List<CourseUserModel>)); // Testing if Model type is Correct
            Assert.IsNotNull(result.Model); //Testing for Null result Model
        }

        [TestMethod()]
        public void GetHomeworksTest()
        {
            //Arrange
            var controller = new StudentController();

            //Act
            var result = controller.GetHomeworks().Result as ViewResult;
            //Assert
            Assert.IsNotNull(result); //Testing for Null result
            Assert.IsInstanceOfType(result, typeof(ViewResult)); // Type Checking
            Assert.IsInstanceOfType(result.Model, typeof(List<AssignedHomework>)); // Testing if Model type is Correct
            Assert.IsNotNull(result.Model); //Testing for Null result Model
        }

        [TestMethod()]
        public void EnrollInCourseTest()
        {
            //Arrange
            var controller = new StudentController();

            //Act
            var result = controller.EnrollInCourse(new CourseUserModel
            {
                CourseID = 1,
                Name = "Math",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            }).Result as ViewResult;
            //Assert
            Assert.IsNotNull(result); //Testing for Null result
            Assert.IsInstanceOfType(result, typeof(ViewResult)); // Type Checking
            Assert.IsNotNull(result.ViewBag); //Testing for Null result Viewbag
            Assert.IsInstanceOfType(result.ViewBag.Message, typeof(string)); // Testing if Model type is Correct

        }

        [TestMethod()]
        public void GetHomeworkPlanTest()
        {
            //Arrange
            var controller = new StudentController();

            //Act
            var result = controller.GetHomeworkPlan().Result as ViewResult;
            //Assert
            Assert.IsNotNull(result); //Testing for Null result
            Assert.IsInstanceOfType(result, typeof(ViewResult)); // Type Checking
            Assert.IsInstanceOfType(result.Model, typeof(List<HomeworkPlanUserModel>)); // Testing if Model type is Correct
            Assert.IsNotNull(result.Model); //Testing for Null result Model
        }

        [TestMethod()]
        public void GeneratePlanTest()
        {
            //Arrange
            var controller = new StudentController();

            //Act
            var result = controller.GeneratePlan().Result as ViewResult;
            //Assert
            Assert.IsNotNull(result); //Testing for Null result
            Assert.IsInstanceOfType(result, typeof(ViewResult)); // Type Checking

        }

        [TestMethod()]
        public void UpdatePlanTest()
        {
            //Arrange
            var controller = new StudentController();

            //Act
            var result = controller.UpdatePlan().Result as ViewResult;
            //Assert
            Assert.IsNotNull(result); //Testing for Null result
            Assert.IsInstanceOfType(result, typeof(ViewResult)); // Type Checking
            Assert.IsInstanceOfType(result.Model, typeof(HomeworkPlanUserModel)); // Testing if Model type is Correct
            Assert.IsNotNull(result.Model); //Testing for Null result Model
        }

        [TestMethod()]
        public void UpdatePlanTest1()
        {
            //Arrange
            var controller = new StudentController();

            //Act
            var result = controller.UpdatePlan(new HomeworkPlanUserModel
            {
                HomeworkPlanID = 1,
                isCompleted = false,
                StudentID = 1,
                HomeworkID = 1,
                PriorityLevel = 1
            }).Result as ViewResult;
            //Assert
            Assert.IsNotNull(result); //Testing for Null result
            Assert.IsInstanceOfType(result, typeof(ViewResult)); // Type Checking
            Assert.IsInstanceOfType(result.Model, typeof(HomeworkPlanUserModel)); // Testing if Model type is Correct
            Assert.IsNotNull(result.Model); //Testing for Null result Model
        }
    }
*/
    }
}