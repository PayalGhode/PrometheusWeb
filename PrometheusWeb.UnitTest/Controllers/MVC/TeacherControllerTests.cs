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
namespace PrometheusWeb.UnitTest.Controllers.MVC
{
    [TestClass]
    public class TeacherControllerTests
    {
        [TestMethod]
        public void ViewTeachersTest()
        {   //Arrange
            TeacherController teacherController = new TeacherController();
            TeacherUserModel teacherUserModel = new TeacherUserModel();
            //Act
            var result = teacherController.ViewTeachers();

            //Assert

            Assert.IsNotNull(result);

        }


        [TestMethod]
        public void AddTeacherTest()
        {   //Arrange
            int Id = 1;
            TeacherController teacherController = new TeacherController();
            TeacherUserModel teacherUserModel = new TeacherUserModel();
            //Act
            var result = teacherController.AddTeacher(Id);

            //Assert

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void AddTeacherTest2()
        {   //Arrange
            
            TeacherController teacherController = new TeacherController();
            AdminUserModel adminUserModel = new AdminUserModel();
            //Act
            var result = teacherController.AddTeacher(adminUserModel);

            //Assert

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void DeleteTeacherTest()
        {   //Arrange
            int id = 1;
            TeacherController teacherController = new TeacherController();
            //Act
            var result = teacherController.DeleteTeacher(id);

            //Assert

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void UpdateTeacherTest()
        {   //Arrange
            int id = 1;
            TeacherController teacherController = new TeacherController();
            //Act
            var result = teacherController.UpdateTeacher(id);

            //Assert

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void UpdateTeacherTest2()
        {   //Arrange
            int id = 1;
            TeacherController teacherController = new TeacherController();
            TeacherUserModel teacherUserModel = new TeacherUserModel();
            //Act
            var result = teacherController.UpdateTeacher(teacherUserModel);

            //Assert

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void UpdateTeacherProfileTest()
        {   //Arrange
            
            TeacherController teacherController = new TeacherController();
            //Act
            var result = teacherController.UpdateTeacherProfile();

            //Assert

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void UpdateTeacherProfileTest2()
        {   //Arrange

            TeacherController teacherController = new TeacherController();
            TeacherUserModel teacherUserModel = new TeacherUserModel();

            //Act
            var result = teacherController.UpdateTeacherProfile(teacherUserModel);

            //Assert

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void SearchTeacherTest()
        {   //Arrange

            TeacherController teacherController = new TeacherController();
            string test = "Test String";
            //Act
            var result = teacherController.SearchTeacher(test);

            //Assert

            Assert.IsNotNull(result);

        }
    }
}
