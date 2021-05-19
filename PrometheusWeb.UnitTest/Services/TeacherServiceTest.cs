using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Services.Services;
using PrometheusWeb.Data.UserModels;

namespace PrometheusWeb.UnitTest.Services
{
    [TestClass]
    public class TeacherServiceTest
    {
        TeacherService teacherService = new TeacherService();


        [TestMethod]
        public void AddTeacherTest()
        {
            //Arrange
            TeacherUserModel teacherUserModel = new TeacherUserModel
            {
                TeacherID = 1,
                FName = "TestName",
                LName = "TestLastName",
                UserID = "TestUserID",
                DOB = DateTime.Now,
                Address = "TestAddress",
                City = "TestCity",
                MobileNo = "TestNumber",
                IsAdmin = false
            };
            //Act
            var result = teacherService.AddTeacher(teacherUserModel);
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void DeleteTeacherTest()
        {
            //Arrange
            int id = 10;
            //Act
            var result = teacherService.DeleteTeacher(id);

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetTeacherTest()
        {
            //Arrange
            int id = 10;
            //Act
            var result = teacherService.GetTeacher(id);
            //Assert
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void GetTeachersTest()
        {
            //Arrange

            //Act
            var result = teacherService.GetTeachers();
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateTeacherTest()

        {
            //Arrange
            int id = 10;
            TeacherUserModel teacherUserModel = new TeacherUserModel
            {
                TeacherID = 1,
                FName = "TestName",
                LName = "TestLastName",
                UserID = "TestUserID",
                DOB = DateTime.Now,
                Address = "TestAddress",
                City = "TestCity",
                MobileNo = "TestNumber",
                IsAdmin = false
            };

            //Act
            var result = teacherService.UpdateTeacher(id, teacherUserModel);

            //Assert
            Assert.IsNotNull(result);


        }

        [TestMethod]
        public void IsTeacherExistsTest()

        { //Arrange
            int id = 10;
            //Act
            var result = teacherService.IsTeacherExists(id);
            //Assert
            Assert.IsNotNull(result);


        }

    }
}
