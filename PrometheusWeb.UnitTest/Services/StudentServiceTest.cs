using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Services.Services;
using PrometheusWeb.Data.UserModels;
namespace PrometheusWeb.UnitTest.Services
{

    [TestClass]
    public class StudentServiceTest
    {
        StudentService studentService = new StudentService();


        [TestMethod]
        public void AddStudentTest()
        {
            //Arrange
            StudentUserModel studentUserModel = new StudentUserModel
            {
                StudentID = 1,
                FName = "TestName",
                LName = "TestLastName",
                UserID = "TestUserID",
                DOB = DateTime.Now,
                Address = "TestAddress",
                City = "TestCity",
                MobileNo = "TestNumber"
            };
            //Act
            var result = studentService.AddStudent(studentUserModel);

            //Assert

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void DeleteStudentTest()
        {
            //Arrange
            int id = 10;
            //Act
            var result = studentService.DeleteStudent(id);
            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetStudentTest()
        {
            //Arrange
            int id = 10;
            //Act
            var result = studentService.GetStudent(id);
            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetStudentsTest()
        {
            //Arrange

            //Act
            var result = studentService.GetStudents();
            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void UpdateStudentTest()
        {
            //Arrange
            int id = 10;
            StudentUserModel studentUserModel = new StudentUserModel
            {
                StudentID = 1,
                FName = "TestName",
                LName = "TestLastName",
                UserID = "TestUserID",
                DOB = DateTime.Now,
                Address = "TestAddress",
                City = "TestCity",
                MobileNo = "TestNumber"
            };
            //Act
            var result = studentService.UpdateStudent(id, studentUserModel);

            //Assert
            Assert.IsTrue(result);
        }
        public void IsStudentExistsTest()
        {
            //Arrange
            int id = 10;
            //Act
            var result = studentService.IsStudentExists(id);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
