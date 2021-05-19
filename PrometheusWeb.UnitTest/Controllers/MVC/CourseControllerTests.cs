using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.MVC.Controllers;
using PrometheusWeb.MVC.Models.ViewModels;
using PrometheusWeb.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PrometheusWeb.UnitTest.Controllers.MVC
{
    [TestClass]
    public class CourseControllerTests
    {
        [TestMethod]
        public void IndexTest()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();

            //Act
            var result = coursesController.Index();

            //
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddCourseTest()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            int id = 1;
            //Act
            var result = coursesController.AddCourse(id);

            //
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddCourseTest2()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            CourseUserModel courseUserModel = new CourseUserModel();
           
            //Act
            var result = coursesController.AddCourse(courseUserModel);

            //
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateCourseTest()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            int id = 1;

            //Act
            var result = coursesController.UpdateCourse(id);

            //
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateCourseTest2()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            CourseUserModel courseUserModel = new CourseUserModel();

            //Act
            var result = coursesController.UpdateCourse(courseUserModel);

            //
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteCourseTest()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            int id = 1;
            //Act
            var result = coursesController.UpdateCourse(id);

            //
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ViewCoursesTest()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
     
            //Act
            var result = coursesController.ViewCourses();

            //
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void SearchCoursesTest()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            string test = "Test String";
            //Act
            var result = coursesController.SearchCourse(test);

            //
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void ViewCoursesEnrollmentTest()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            int id = 1;
            //Act
            var result = coursesController.ViewCoursesEnrollment(id);

            //
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void StudentCoursesTest()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            int id = 1;
            //Act
            var result = coursesController.StudentCourses(id);

            //
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void EnrollInCourseTest()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            CourseUserModel courseUserModel = new CourseUserModel();

            //Act
            var result = coursesController.EnrollInCourse(courseUserModel);

            //
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void TeacherCoursesTest()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            int id = 1;

            //Act
            var result = coursesController.TeacherCourses(id);

            //
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void ViewCoursesForTeachingTest()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            

            //Act
            var result = coursesController.ViewCoursesForTeaching();

            //
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void SaveCoursesTest()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            int id1 = 1;
            int id2 = 1;

            //Act
            var result = coursesController.SaveCourses(id1,id2);

            //
            Assert.IsNotNull(result);
        }
        


        [TestMethod]
        public void SaveCoursesTest2()
        {
            //Arrange 
            CoursesController coursesController = new CoursesController();
            CourseUserModel courseUserModel = new CourseUserModel();
           
            //Act
            var result = coursesController.SaveCourses(courseUserModel);

            //
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void DeleteCourseTest2()
        //{
        //    //Arrange 
        //    CoursesController coursesController = new CoursesController();
        //    CourseUserModel courseUserModel = new CourseUserModel();

        //    //Act
        //    var result = coursesController.UpdateCourse(courseUserModel);

        //    //
        //    Assert.IsNotNull(result);
        //}
    }
}
