using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Services.Services;
using PrometheusWeb.Data.UserModels;
namespace PrometheusWeb.UnitTest.Services
{
    [TestClass]
    public class TeachesServiceTest
    {
        TeachesService teachesService = new TeachesService();
        [TestMethod]
        public void AddTeacherCourseTest()
        {
            //Arrange

            TeacherCourseUserModel teacherCourseUserModel = new TeacherCourseUserModel
            {
                CourseID = 1,
                TeacherID = 1
            };
            //Act
            var result = teachesService.AddTeacherCourse(teacherCourseUserModel);
            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void DeleteTeacherCourseTest()
        {
            //Arrange
            int id = 10;
            //Act
            var result = teachesService.DeleteTeacherCourse(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTeacherCoursesTest()
        {
            //Arrange

            //Act
            var result = teachesService.GetTeacherCourses();

            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetTeacherCoursesTest1()
        {
            //Arrange
            int id = 10;
            //Act
            var result = teachesService.GetTeacherCourses(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateTeacherCoursesTest()
        {
            //Arrange
            int id = 10;
            TeacherCourseUserModel teacherCourseUserModel = new TeacherCourseUserModel
            {
                CourseID = 1,
                TeacherID = 1,
                TeacherCourseID = 1
            };
            //Act
            var result = teachesService.UpdateTeacherCourses(id, teacherCourseUserModel);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsTeachesExistsTest()
        {
            //Arrange
            int id = 10;
            //Act
            var result = teachesService.IsTeachesExists(id);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
