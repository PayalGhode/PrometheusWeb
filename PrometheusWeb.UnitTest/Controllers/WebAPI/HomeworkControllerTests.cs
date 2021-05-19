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
    public class HomeworkControllerTests
    {
        public readonly IHomeworkService MockProductsRepository;
        public HomeworkControllerTests()
        {
            // create some mock Items to play with
            IQueryable<HomeworkUserModel> enrollments = new List<HomeworkUserModel>
                {
                    new HomeworkUserModel
                    {
                        HomeWorkID = 1,
                        Description = "Something",
                        LongDescription = "Somthing Something",
                        Deadline = DateTime.Now,
                        ReqTime = DateTime.Now
                    },
                    new HomeworkUserModel
                    {
                        HomeWorkID = 2,
                        Description = "Something New",
                        LongDescription = "Somthing Something New",
                        Deadline = DateTime.Now,
                        ReqTime = DateTime.Now
                    }
                }.AsQueryable();
            // Mock the Homework Repository using Moq
            Mock<IHomeworkService> mockProductRepository = new Mock<IHomeworkService>();

            // Return all the items
            mockProductRepository.Setup(mr => mr.GetHomeworks()).Returns(enrollments);

            // return a item by Id
            mockProductRepository.Setup(mr => mr.GetHomework(
                It.IsAny<int>())).Returns((int i) => enrollments.Where(
                x => x.HomeWorkID == i).Single());

            // Add a item   
            mockProductRepository.Setup(mr => mr.AddHomework(
                It.IsAny<HomeworkUserModel>())).Returns((HomeworkUserModel target) => {
                    target.HomeWorkID = enrollments.Count() + 1;
                    enrollments.ToList().Add(target);
                    return true;
                });
            // Update a item   
            mockProductRepository.Setup(mr => mr.UpdateHomework(It.IsAny<int>(), It.IsAny<HomeworkUserModel>())).Returns((int id, HomeworkUserModel target) => {
                var original = enrollments.Where(
                        q => q.HomeWorkID == target.HomeWorkID).Single();

                if (original == null)
                {
                    return false;
                }

                original.HomeWorkID = target.HomeWorkID;
                original.Deadline = target.Deadline;
                original.Description = target.Description;
                original.LongDescription = target.LongDescription;
                original.ReqTime = target.ReqTime;

                return true;
            });

            // Delete a item   
            mockProductRepository.Setup(mr => mr.DeleteHomework(
                It.IsAny<int>())).Returns((int id) => {
                    var original = enrollments.Where(
                            q => q.HomeWorkID == id).Single();
                    enrollments.ToList().Remove(original);
                    return original;
                });

            // Complete the setup of our Mock Homework Repository
            this.MockProductsRepository = mockProductRepository.Object;
        }

        [TestMethod()]
        public void GetHomeworksTest()
        {
            //Arrange
            HomeworkController controller = new HomeworkController(this.MockProductsRepository);

            //Act
            var result = controller.GetHomeworks();

            //Assert
            Assert.IsNotNull(result); // Test if null
            Assert.IsInstanceOfType(result, typeof(IQueryable<HomeworkUserModel>)); // Test type
        }

        [TestMethod()]
        public void GetHomeworkTest()
        {
            //Arrange
            HomeworkController controller = new HomeworkController(this.MockProductsRepository);
            int id = 1;

            //Act
            var result = controller.GetHomework(id);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
                                                                        // Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type
        }

        [TestMethod()]
        public void PutHomeworkTest()
        {
            //Arrange
            HomeworkController controller = new HomeworkController(this.MockProductsRepository);
            int id = 1;
            HomeworkUserModel userModel = new HomeworkUserModel
            {
                HomeWorkID = id,
                Description = "Inheritance",
                LongDescription = "Implement All Types",
                Deadline = DateTime.Now,
                ReqTime = DateTime.Now,

            };
            //Act
            var result = controller.PutHomework(id, userModel);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
            Assert.IsInstanceOfType(result, typeof(OkResult)); // Test type
        }

        [TestMethod()]
        public void PostHomeworkTest()
        {
            //Arrange
            HomeworkController controller = new HomeworkController(this.MockProductsRepository);
            int id = 1;
            HomeworkUserModel userModel = new HomeworkUserModel
            {
                HomeWorkID = id,
                Description = "Inheritance",
                LongDescription = "Implement All Types",
                Deadline = DateTime.Now,
                ReqTime = DateTime.Now,

            };
            //Act
            var result = controller.PostHomework(userModel);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
        }

        [TestMethod()]
        public void DeleteHomeworkTest()
        {
            //Arrange
            HomeworkController controller = new HomeworkController(this.MockProductsRepository);
            int id = 1;

            //Act
            var result = controller.DeleteHomework(id);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
                                                                        // Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type

        }
    }
}