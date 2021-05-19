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
    public class HomeControllerTests
    {
        [TestMethod]
        public void IndexTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            //Act
            var result = homeController.Index();

            //
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AboutTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            //Act
            var result = homeController.About();

            //
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ContactTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            //Act
            var result = homeController.Contact();

            //
            Assert.IsNotNull(result);
        }

    }
}
