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
    public class AdminControllerTests
    {
        [TestMethod]
        public void IndexTest()
        {
            //Arrange 
            AdminController adminController = new AdminController();

            //Act
            var result = adminController.Index();

            //
            Assert.IsNotNull(result);
        }
    }
}
