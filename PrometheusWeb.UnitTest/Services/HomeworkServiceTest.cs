using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Services.Services;
using PrometheusWeb.Data.UserModels;
namespace PrometheusWeb.UnitTest.Services
{
    [TestClass]
    public class HomeworkServiceTest
    {
        HomeworkService homeworkService = new HomeworkService();

        [TestMethod]
        public void AddHomeworkTest()
        {
            // Arrange
            HomeworkUserModel homeworkUserModel = new HomeworkUserModel
            {
                HomeWorkID = 1,
                Description = "Test Description",
                Deadline = DateTime.Now,
                ReqTime = DateTime.Now,
                LongDescription = "test"
            };
            //Act
            var result = homeworkService.AddHomework(homeworkUserModel);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteHomeworkTest()
        {
            // Arrange
            int id = 10;

            //Act
            var result = homeworkService.DeleteHomework(id);

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetHomeworkTest()
        {
            // Arrange
            int id = 10;

            //Act
            var result = homeworkService.GetHomework(id);

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetHomeworksTest()
        {
            // Arrange


            //Act
            var result = homeworkService.GetHomeworks();

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void UpdateHomeworkTest()
        {
            // Arrange
            int id = 10;
            HomeworkUserModel homeworkUserModel = new HomeworkUserModel
            {
                HomeWorkID = 1,
                Description = "Test",
                Deadline = DateTime.Now,
                ReqTime = DateTime.Now,
                LongDescription = "Test Description"

            };

            //Act
            var result = homeworkService.UpdateHomework(id, homeworkUserModel);

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsHomeworkExistsTest()
        {
            //Arrange
            int id = 10;

            //Act
            var result = homeworkService.IsHomeworkExists(id);

            //Assert
            Assert.IsTrue(result);

        }
    }
}
