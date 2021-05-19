using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.Services.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrometheusWeb.Data.DataModels;

namespace PrometheusWeb.UnitTest.Services
{
    [TestClass]
    public class HomeworkPlanServiceTest
    {
        HomeworkPlanService homeworkPlanService = new HomeworkPlanService();

        [TestMethod]
        public void GetHomeworkPlanTest()
        {
            //Arrange 
            int id = 10;
            //Act
            var result = homeworkPlanService.GetHomeworkPlan(id);
            //Assert
            Assert.IsNotNull(result);


        }
        [TestMethod]
        public void GetHomeworkPlansTest()
        {
            //Arrange 

            //Act
            var result = homeworkPlanService.GetHomeworkPlans();
            //Assert
            Assert.IsNotNull(result);


        }
        [TestMethod]
        public void GetHomeworkPlansTest1()
        {
            //Arrange 
            int id = 10;
            //Act
            var result = homeworkPlanService.GetHomeworkPlan(id);
            //Assert
            Assert.IsNotNull(result);


        }

        [TestMethod]
        public void AddHomeworkPlanTest()
        {
            //Arrange 
            HomeworkPlanUserModel homeworkPlanUserModel = new HomeworkPlanUserModel
            {
                HomeworkID = 20,
                StudentID = 1,
                PriorityLevel = 5,
                isCompleted = false
            };

            //Act
            var result = homeworkPlanService.AddHomeworkPlan(homeworkPlanUserModel);
            //Assert
            Assert.IsTrue(result);


        }
        [TestMethod]
        public void AddHomeworkPlansTest()
        {
            //Arrange 

            IQueryable<HomeworkPlanUserModel> homeworkPlanUserModel = new List<HomeworkPlanUserModel>
            {
                     new HomeworkPlanUserModel
                { HomeworkID = 1,
                StudentID = 1,
                PriorityLevel = 1,
                isCompleted = true

                }
            }.AsQueryable();

            //Act
            var result = homeworkPlanService.AddHomeworkPlans(homeworkPlanUserModel);

            //Assert
            Assert.IsTrue(result);


        }
        [TestMethod]
        public void UpdateHomeworkPlanTest()
        {
            //Arrange 
            int id = 10;
            HomeworkPlanUserModel homeworkPlanUserModel = new HomeworkPlanUserModel
            {
                HomeworkPlanID = 1,
                HomeworkID = 1,
                StudentID = 1,
                PriorityLevel = 1,
                isCompleted = true
            };

            //Act
            var result = homeworkPlanService.UpdateHomeworkPlan(id, homeworkPlanUserModel);
            //Assert
            Assert.IsTrue(result);


        }
        [TestMethod]
        public void UpdateHomeworkPlansTest()
        {
            //Arrange 


            IQueryable<HomeworkPlanUserModel> homeworkPlanUserModel = new List<HomeworkPlanUserModel>
            {
                     new HomeworkPlanUserModel
                { HomeworkID = 1,
                StudentID = 1,
                PriorityLevel = 1,
                isCompleted = true

                }
            }.AsQueryable();

            //Act
            var result = homeworkPlanService.UpdateHomeworkPlans(homeworkPlanUserModel);
            //Assert
            Assert.IsTrue(result);


        }
        [TestMethod]
        public void DeleteHomeworkPlanTest()
        {
            //Arrange 
            int id = 10;


            //Act
            var result = homeworkPlanService.DeleteHomeworkPlan(id);


            //Assert
            Assert.IsNotNull(result);


        }
        [TestMethod]
        public void DeleteHomeworkPlansTest()
        {
            //Arrange 
            int id = 10;


            //Act
            var result = homeworkPlanService.DeleteHomeworkPlans(id);


            //Assert
            Assert.IsTrue(result);


        }
        [TestMethod]
        public void IsHomeworkPlanExistsTest()
        {
            //Arrange 
            int id = 10;


            //Act
            var result = homeworkPlanService.IsHomeworkPlanExists(id);


            //Assert
            Assert.IsTrue(result);
        }
    }
}
