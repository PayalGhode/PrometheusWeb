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
    public class TeachersControllerTests
    {
        public readonly ITeacherService MockProductsRepository;
        public TeachersControllerTests()
        {

            // create some mock items to play with
            IQueryable<TeacherUserModel> enrollments = new List<TeacherUserModel>
                {
                    new TeacherUserModel
                    {
                        TeacherID = 1,
                        FName = "Ram",
                        LName = "Sharma",
                        DOB = DateTime.Now,
                        UserID = "New1919",
                        Address  = "XYZ",
                        City = "LV",
                        MobileNo = "0000000000",
                        IsAdmin = true
                    },
                    new TeacherUserModel
                    {
                        TeacherID = 2,
                        FName = "Shyam",
                        LName = "Verma",
                        DOB = DateTime.Now,
                        UserID = "New2021",
                        Address  = "XYZ2",
                        City = "LV1",
                        MobileNo = "1111111111",
                        IsAdmin = false
                    }
                }.AsQueryable();
            // Mock the Teacher Repository using Moq
            Mock<ITeacherService> mockProductRepository = new Mock<ITeacherService>();

            // Return all the items
            mockProductRepository.Setup(mr => mr.GetTeachers()).Returns(enrollments);

            // return a item by Id
            mockProductRepository.Setup(mr => mr.GetTeacher(
                It.IsAny<int>())).Returns((int i) => enrollments.Where(
                x => x.TeacherID == i).Single());

            // Add a item   
            mockProductRepository.Setup(mr => mr.AddTeacher(
                It.IsAny<TeacherUserModel>())).Returns((TeacherUserModel target) => {
                    target.TeacherID = enrollments.Count() + 1;
                    enrollments.ToList().Add(target);
                    return true;
                });
            // Update a item   
            mockProductRepository.Setup(mr => mr.UpdateTeacher(It.IsAny<int>(), It.IsAny<TeacherUserModel>())).Returns((int id, TeacherUserModel target) => {
                var original = enrollments.Where(
                        q => q.TeacherID == target.TeacherID).Single();

                if (original == null)
                {
                    return false;
                }

                original.TeacherID = target.TeacherID;
                original.FName = target.FName;
                original.LName = target.LName;
                original.DOB = target.DOB;
                original.Address = target.Address;
                original.City = target.City;
                original.UserID = target.UserID;
                original.MobileNo = target.MobileNo;
                original.IsAdmin = target.IsAdmin;

                return true;
            });

            // Delete a item   
            mockProductRepository.Setup(mr => mr.DeleteTeacher(
                It.IsAny<int>())).Returns((int id) => {
                    var original = enrollments.Where(
                            q => q.TeacherID == id).Single();
                    enrollments.ToList().Remove(original);
                    return original;
                });

            // Complete the setup of our Mock Teacher Repository
            this.MockProductsRepository = mockProductRepository.Object;
        }

        [TestMethod()]
        public void GetTeachersTest()
        {
            //Arrange
            TeachersController controller = new TeachersController(this.MockProductsRepository);

            //Act
            var result = controller.GetTeachers();

            //Assert
            Assert.IsNotNull(result); // Test if null
            Assert.IsInstanceOfType(result, typeof(IQueryable<EnrollmentUserModel>)); // Test type
        }

        [TestMethod()]
        public void GetTeacherTest()
        {
            //Arrange
            TeachersController controller = new TeachersController(this.MockProductsRepository);
            int id = 1;

            //Act
            var result = controller.GetTeacher(id);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
                                                                        // Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type
        }

        [TestMethod()]
        public void PutTeacherTest()
        {
            //Arrange
            TeachersController controller = new TeachersController(this.MockProductsRepository);
            int id = 1;
            TeacherUserModel userModel = new TeacherUserModel
            {
                TeacherID = 1,
                FName = "Ram",
                LName = "Sharma",
                DOB = DateTime.Now,
                UserID = "New1919",
                Address = "XYZ",
                City = "LV",
                MobileNo = "0000000000",
                IsAdmin = true
            };
            
            //Act
            var result = controller.PutTeacher(id, userModel);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
            Assert.IsInstanceOfType(result, typeof(OkResult)); // Test type
        }

        [TestMethod()]
        public void PostTeacherTest()
        {
            //Arrange
            TeachersController controller = new TeachersController(this.MockProductsRepository);
            int id = 1;
            TeacherUserModel userModel = new TeacherUserModel
            {
                TeacherID = 1,
                FName = "Ram",
                LName = "Sharma",
                DOB = DateTime.Now,
                UserID = "New1919",
                Address = "XYZ",
                City = "LV",
                MobileNo = "0000000000",
                IsAdmin = true
            };
            //Act
            var result = controller.PostTeacher(userModel);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
        }

        [TestMethod()]
        public void DeleteTeacherTest()
        {
            //Arrange
            TeachersController controller = new TeachersController(this.MockProductsRepository);
            int id = 1;

            //Act
            var result = controller.DeleteTeacher(id);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object

        }
    }
}