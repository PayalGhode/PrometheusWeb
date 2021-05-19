using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.Services.Controllers;
using PrometheusWeb.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace PrometheusWeb.Services.Controllers.Tests
{
    [TestClass()]
    public class HomeworkPlansControllerTests
    {
        public readonly IHomeworkPlanService MockProductsRepository;
        public HomeworkPlansControllerTests()
        {

            // create some mock Items to play with
            IQueryable<HomeworkPlanUserModel> enrollments = new List<HomeworkPlanUserModel>
                {
                    new HomeworkPlanUserModel
                    {
                        HomeworkPlanID = 1,
                        isCompleted = false,
                        StudentID = 1,
                        HomeworkID = 1,
                        PriorityLevel = 1
                    },
                    new HomeworkPlanUserModel
                    {
                        HomeworkPlanID = 2,
                        isCompleted = false,
                        StudentID = 1,
                        HomeworkID = 2,
                        PriorityLevel = 2
                    }
                }.AsQueryable();
            // Mock the HomeworkPlan Repository using Moq
            Mock<IHomeworkPlanService> mockProductRepository = new Mock<IHomeworkPlanService>();

            // Return all the items
            mockProductRepository.Setup(mr => mr.GetHomeworkPlans()).Returns(enrollments);

            // return a item by Id
            mockProductRepository.Setup(mr => mr.GetHomeworkPlan(
                It.IsAny<int>())).Returns((int i) => enrollments.Where(
                x => x.HomeworkPlanID == i).Single());

            // Add a item   
            mockProductRepository.Setup(mr => mr.AddHomeworkPlan(
                It.IsAny<HomeworkPlanUserModel>())).Returns((HomeworkPlanUserModel target) => {
                    target.HomeworkPlanID = enrollments.Count() + 1;
                    enrollments.ToList().Add(target);
                    return true;
                });
            // Update a item   
            mockProductRepository.Setup(mr => mr.UpdateHomeworkPlan(It.IsAny<int>(), It.IsAny<HomeworkPlanUserModel>())).Returns((int id, HomeworkPlanUserModel target) => {
                var original = enrollments.Where(
                        q => q.HomeworkPlanID == target.HomeworkPlanID).Single();

                if (original == null)
                {
                    return false;
                }

                original.HomeworkPlanID = target.HomeworkPlanID;
                original.HomeworkID = target.HomeworkID;
                original.StudentID = target.StudentID;
                original.isCompleted = target.isCompleted;
                original.PriorityLevel = target.PriorityLevel;

                return true;
            });

            // Delete a item   
            mockProductRepository.Setup(mr => mr.DeleteHomeworkPlan(
                It.IsAny<int>())).Returns((int id) => {
                    var original = enrollments.Where(
                            q => q.HomeworkPlanID == id).Single();
                    enrollments.ToList().Remove(original);
                    return original;
                });

            // Complete the setup of our Mock HomeworkPlan Repository
            this.MockProductsRepository = mockProductRepository.Object;
        }

        [TestMethod()]
        public void GetHomeworkPlansTest()
        {
            //Arrange
            HomeworkPlansController controller = new HomeworkPlansController(this.MockProductsRepository);

            //Act
            var result = controller.GetHomeworkPlans();

            //Assert
            Assert.IsNotNull(result); // Test if null
            //Assert.IsInstanceOfType(result, typeof(IQueryable<HomeworkPlanUserModel>)); // Test type
        }

        [TestMethod()]
        public void GetHomeworkPlansTest1()
        {
            //Arrange
            HomeworkPlansController controller = new HomeworkPlansController(this.MockProductsRepository);

            //Act
            var result = controller.GetHomeworkPlans();

            //Assert
            //Assert.IsNotNull(result); // Test if null
            Assert.IsInstanceOfType(result, typeof(IQueryable<HomeworkPlanUserModel>)); // Test type
        }

        [TestMethod()]
        public void GetHomeworkPlanTest()
        {
            //Arrange
            HomeworkPlansController controller = new HomeworkPlansController(this.MockProductsRepository);
            int id = 1;

            //Act
            var result = controller.GetHomeworkPlan(id);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
        }

        [TestMethod()]
        public void PutHomeworkPlanTest()
        {
            //Arrange
            HomeworkPlansController controller = new HomeworkPlansController(this.MockProductsRepository);
            int id = 1;
            HomeworkPlanUserModel userModel = new HomeworkPlanUserModel
            {
                HomeworkPlanID = id,
                HomeworkID = 2,
                isCompleted = true,
                PriorityLevel = 9
            };
            //Act
            var result = controller.PutHomeworkPlan(id, userModel);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
            Assert.IsInstanceOfType(result, typeof(OkResult)); // Test type
        }

        [TestMethod()]
        public void PostHomeworkPlanTest()
        {
            //Arrange
            HomeworkPlansController controller = new HomeworkPlansController(this.MockProductsRepository);
            int id = 1;
            HomeworkPlanUserModel userModel = new HomeworkPlanUserModel
            {
                
                HomeworkID = 2,
                isCompleted = true,
                PriorityLevel = 9
            };
            //Act
            var result = controller.PostHomeworkPlan(userModel);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult));
        }

        

        [TestMethod()]
        public void DeleteHomeworkPlansTest()
        {
            //Arrange
            HomeworkPlansController controller = new HomeworkPlansController(this.MockProductsRepository);
            int id = 1;

            //Act
            var result = controller.DeleteHomeworkPlan(id);

            //Assert
            Assert.IsNotNull(result); // Test if null
        }
    }
}