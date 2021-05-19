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
    public class HomeworkControllerTests
    {

        [TestMethod]
        public void IndexTest()
        {
            //Arrange 
            HomeworkController homeworkController = new HomeworkController();
            //Act
            var result = homeworkController.Index();

            //
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void ViewHomeworksTest()
        {
            //Arrange 
            HomeworkController homeworkController = new HomeworkController();
            //Act
            var result = homeworkController.ViewHomeworks();

            //
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddOrEditHomeworksTest()
        {
            //Arrange 
            HomeworkController homeworkController = new HomeworkController();
           
            //Act
            var result = homeworkController.AddHomeworks();

            //
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddOrEditHomeworksTest2()
        {
            //Arrange 
            HomeworkController homeworkController = new HomeworkController();
            HomeworkUserModel homeworkUserModel = new HomeworkUserModel();
            //Act
            var result = homeworkController.AddHomeworks(homeworkUserModel);

            //
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void DeleteTest()
        {
            //Arrange 
            HomeworkController homeworkController = new HomeworkController();
            int id = 1;
            //Act
            var result = homeworkController.Delete(id);

            //
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void GetHomeworksOfStudentTest()
        {
            //Arrange 
            HomeworkController homeworkController = new HomeworkController();
            int id = 1;
            //Act
            var result = homeworkController.GetHomeworksOfStudent(id);

            //
            Assert.IsNotNull(result);
        }




        [TestMethod]
        public void GetHomeworkPlanTest()
        {
            //Arrange 
            HomeworkController homeworkController = new HomeworkController();
            int id = 1;
            //Act
            var result = homeworkController.GetHomeworkPlan(id);

            //
            Assert.IsNotNull(result);
        }





        [TestMethod]
        public void GenerateHomeworkPlanTest()
        {
            //Arrange 
            HomeworkController homeworkController = new HomeworkController();
            int id = 1;
            //Act
            var result = homeworkController.GenerateHomeworkPlan(id);

            //
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void UpdateHomeworkPlanTest()
        {
            //Arrange 
            HomeworkController homeworkController = new HomeworkController();
            int id = 1;
            //Act
            var result = homeworkController.UpdateHomeworkPlan(id);

            //
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void UpdateHomeworkPlanTest2()
        {
            //Arrange 
            HomeworkController homeworkController = new HomeworkController();
            HomeworkPlanUserModel homeworkPlanUserModel = new HomeworkPlanUserModel();
            //Act
            var result = homeworkController.UpdateHomeworkPlan(homeworkPlanUserModel);

            //
            Assert.IsNotNull(result);
        }




        [TestMethod]
        public void AssignHomeworkTest()
        {
            //Arrange 
            HomeworkController homeworkController = new HomeworkController();
            HomeworkPlanUserModel homeworkPlanUserModel = new HomeworkPlanUserModel();
            int id = 1;
            //Act
            var result = homeworkController.AssignHomework(id);

            //
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void AssignHomeworkTest2()
        {
            //Arrange 
            HomeworkController homeworkController = new HomeworkController();
            AssignmentUserModel assignmentUserModel = new AssignmentUserModel();
            int id = 10;
            //Act
            var result = homeworkController.AssignHomework(id);

            //
            Assert.IsNotNull(result);
        }

    }
}
