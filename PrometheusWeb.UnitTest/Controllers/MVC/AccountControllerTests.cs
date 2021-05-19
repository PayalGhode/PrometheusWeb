using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.MVC.Controllers;
using PrometheusWeb.MVC.Models.ViewModels;
using PrometheusWeb.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace PrometheusWeb.UnitTest.Controllers.MVC
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void LoginTest()
        {
            //Arrange
            AccountController accountController = new AccountController();
            string test = "Test Login";
            //Act 
            var result = accountController.Login(test);
            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Second Login  method not verified
        /// </summary>
        [TestMethod]
        public void LoginTest2()
        {
            //Arrange
            AccountController accountController = new AccountController();
            LoginModel loginModel = new LoginModel();
            string test = "Test Login";
            //Act 
            var result = accountController.Login(loginModel,test);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VerifyCodeTest()
        {
            //Arrange
            AccountController accountController = new AccountController();
            string test = "Test String";
            string test2 = "Test String";
            bool test3 = true;
            //Act 
            var result = accountController.VerifyCode(test,test2,test3);
            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void VerifyCodeTest2()
        {
            //Arrange
            AccountController accountController = new AccountController();
            VerifyCodeViewModel verifyCodeViewModel = new VerifyCodeViewModel();
     
            //Act 
            var result = accountController.VerifyCode(verifyCodeViewModel);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RegisterTest()
        {
            //Arrange
            AccountController accountController = new AccountController();
           

            //Act 
            var result = accountController.Register();
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RegisterTest2()
        {
            //Arrange
            AccountController accountController = new AccountController();
            RegisterViewModel registerViewModel = new RegisterViewModel();

            //Act 
            var result = accountController.Register(registerViewModel);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RegisterTeacherTest()
        {
            //Arrange
            AccountController accountController = new AccountController();
            RegisterViewModel registerViewModel = new RegisterViewModel();

            //Act 
            var result = accountController.RegisterTeacher();
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RegisterStudentTest()
        {
            //Arrange
            AccountController accountController = new AccountController();
            RegisterViewModel registerViewModel = new RegisterViewModel();

            //Act 
            var result = accountController.RegisterStudent();
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RegisterTeacherTest2()
        {
            //Arrange
            AccountController accountController = new AccountController();
            RegisterViewModelTeacher registerViewModelTeacher = new RegisterViewModelTeacher();
            //Act 
             var result = accountController.RegisterTeacher(registerViewModelTeacher);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RegisterStudentTest2()
        {
            //Arrange
            AccountController accountController = new AccountController();
            RegisterViewModelStudent registerViewModelStudent = new RegisterViewModelStudent();
            //Act 
            var result = accountController.RegisterStudent(registerViewModelStudent);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ConfirmEmailTest()
        {
            //Arrange
            AccountController accountController = new AccountController();
            string test = "Test userID";
            string test2 = "Test Code";
            //Act 
            var result = accountController.ConfirmEmail(test,test2);
            //Assert
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void ForgotPasswordTest()
        //{
        //    //Arrange
        //    AccountController accountController = new AccountController();
           
        //    var result = accountController.ForgotPassword();
        //    //Assert
        //    Assert.IsNotNull(result);
        //}
        //[TestMethod]
        //public void ForgotPasswordTest2()
        //{
        //    //Arrange
        //    AccountController accountController = new AccountController();
        //    ForgotPasswordViewModel forgotPasswordViewModel = new ForgotPasswordViewModel();
        //    string id = "TestId";
        //    //Act
        //    var result = accountController.ForgotPasswordAsync(id);
        //    //Assert
        //    Assert.IsNotNull(result);
        //}

        [TestMethod]
        public void ForgotPasswordConfirmationTest()
        {
            AccountController accountController = new AccountController();

            //Act
            var result = accountController.ForgotPasswordConfirmation();
            //Assert
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void ResetPasswordTest()
        //{
        //    AccountController accountController = new AccountController();
        //    ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel();
        //    //Act
        //    var result = accountController.ResetPassword(resetPasswordViewModel);
        //    //Assert
        //    Assert.IsNotNull(result);
        //}
    }
}
