using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Services.Services;
using PrometheusWeb.Data.UserModels;

namespace PrometheusWeb.UnitTest.Services
{

    [TestClass]
    public class UserServiceTest
    {
        UserService userService = new UserService();

        [TestMethod]
        public void DeleteUserTest()
        {
            //Arrange
            string id = "10";
            //Act
            var result = userService.DeleteUser(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUserTest()
        {
            //Arrange
            string id = "10";
            //Act
            var result = userService.GetUser(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUsersTest()
        {
            //Arrange

            //Act
            var result = userService.GetUsers();

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AddUserTest()
        {
            //Arrange
            AdminUserModel adminUserModel = new AdminUserModel
            {
                UserID = "TestID",
                Password = "TestPassword",
                Role = "Role",
                SecurityQuestion = "TestQuestion",
                SecurityAnswer = "TestAnswer"
            };
            //Act
            var result = userService.AddUser(adminUserModel);

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void UpdateUserTest()
        {
            //Arrange
            string id = "10";
            AdminUserModel adminUserModel = new AdminUserModel
            {
                UserID = "TestID",
                Password = "TestPassword",
                Role = "Role",
                SecurityQuestion = "TestQuestion",
                SecurityAnswer = "TestAnswer"
            };
            //Act
            var result = userService.UpdateUser(id, adminUserModel);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
