using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrometheusWeb.Services.Services.Tests
{
    [TestClass()]
    public class CourseServiceTests
    {
        CourseService courseService = new CourseService();
        [TestMethod()]
        public void AddCourseTest()
        {
            //Arrange

            CourseUserModel userModel = new CourseUserModel
            {
                Name = "Test",
                CourseID = 2120,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            //Act
            var result = courseService.AddCourse(userModel);

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void DeleteCourseTest()
        {
            //Arrange
            int id = 10;
            //Act
            var result = courseService.DeleteCourse(id);

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void getCourseTest()
        {
            //Arrange
            int id = 10;
            //Act
            var result = courseService.GetCourse(id);

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void GetCoursesTest()
        {
            //Arrange

            //Act
            var result = courseService.GetCourses();

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void UpdateCourseTest()
        {
            int id = 10;
            //Arrange
            CourseUserModel course = new CourseUserModel
            {
                CourseID = 1,
                Name = "Test",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            //Act
            var result = courseService.UpdateCourse(id, course);

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]

        public void IsCourseExistsTest()
        {
            //Arrange 
            int id = 10;
            //Act
            var result = courseService.IsCourseExists(id);
            //Assert
            Assert.IsTrue(result);

        }
    }
}