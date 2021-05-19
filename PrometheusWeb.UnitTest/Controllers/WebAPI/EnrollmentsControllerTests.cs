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
    public class EnrollmentsControllerTests
    {
        public readonly IEnrollmentService MockProductsRepository;
        public EnrollmentsControllerTests()
        {

            // create some mock Items to play with
            IQueryable<EnrollmentUserModel> enrollments = new List<EnrollmentUserModel>
                {
                    new EnrollmentUserModel
                    {
                        EnrollmentID = 1,
                        CourseID = 1,
                        StudentID = 1
                    },
                    new EnrollmentUserModel
                    {
                        EnrollmentID = 2,
                        CourseID = 2,
                        StudentID = 1
                    }
                }.AsQueryable();
            // Mock the Enrollment Repository using Moq
            Mock<IEnrollmentService> mockProductRepository = new Mock<IEnrollmentService>();

            // Return all the items
            mockProductRepository.Setup(mr => mr.GetEnrollments()).Returns(enrollments);

            // return a item by Id
            mockProductRepository.Setup(mr => mr.GetEnrollment(
                It.IsAny<int>())).Returns((int i) => enrollments.Where(
                x => x.EnrollmentID == i).Single());

            // Add a item   
            mockProductRepository.Setup(mr => mr.AddEnrollment(
                It.IsAny<EnrollmentUserModel>())).Returns((EnrollmentUserModel target) => {               
                    target.EnrollmentID = enrollments.Count() + 1;
                    enrollments.ToList().Add(target);
                    return true;
                });
            // Update a item   
            mockProductRepository.Setup(mr => mr.UpdateEnrollment(It.IsAny<int>(),It.IsAny<EnrollmentUserModel>())).Returns((int id,EnrollmentUserModel target) => {
                    var original = enrollments.Where(
                            q => q.EnrollmentID == target.EnrollmentID).Single();

                    if (original == null)
                    {
                        return false;
                    }

                    original.EnrollmentID = target.EnrollmentID;
                    original.CourseID = target.CourseID;
                    original.StudentID = target.StudentID;

                    return true;
                });

            // Delete a item   
            mockProductRepository.Setup(mr => mr.DeleteEnrollment(
                It.IsAny<int>())).Returns((int id) => {
                    var original = enrollments.Where(
                            q => q.EnrollmentID == id).Single();
                    enrollments.ToList().Remove(original);
                    return original;
                });

            // Complete the setup of our Mock Enrollment Repository
            this.MockProductsRepository = mockProductRepository.Object;
        }

        [TestMethod()]
        public void GetEnrollmentsTest()
        {
            //Arrange
            EnrollmentsController controller = new EnrollmentsController(this.MockProductsRepository);

            //Act
            var result = controller.GetEnrollments();

            //Assert
            Assert.IsNotNull(result); // Test if null
            Assert.IsInstanceOfType(result, typeof(IQueryable<EnrollmentUserModel>)); // Test type
        }

        [TestMethod()]
        public void GetEnrollmentTest()
        {
            //Arrange
            EnrollmentsController controller = new EnrollmentsController(this.MockProductsRepository);
            int id = 1;

            //Act
            var result = controller.GetEnrollment(id);

            //Assert
            Assert.IsNotNull(result); // Test if null
            
            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
           // Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type
        }

        [TestMethod()]
        public void PostEnrollmentTest()
        {
            //Arrange
            EnrollmentsController controller = new EnrollmentsController(this.MockProductsRepository);
            int id = 1;
            EnrollmentUserModel userModel = new EnrollmentUserModel
            {
                CourseID = 2,
                StudentID = 1
            };
            //Act
            var result = controller.PostEnrollment(userModel);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
            
        }

        [TestMethod()]
        public void PutEnrollmentTest()
        {
            //Arrange
            EnrollmentsController controller = new EnrollmentsController(this.MockProductsRepository);
            int id = 1;
            EnrollmentUserModel userModel = new EnrollmentUserModel
            {
                EnrollmentID = id,
                CourseID = 2,
                StudentID = 1
            };
            //Act
            var result = controller.PutEnrollment(id,userModel);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
            Assert.IsInstanceOfType(result, typeof(OkResult)); // Test type
        }

        [TestMethod()]
        public void DeleteEnrollmentTest()
        {
            //Arrange
            EnrollmentsController controller = new EnrollmentsController(this.MockProductsRepository);
            int id = 1;

            //Act
            var result = controller.DeleteEnrollment(id);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
                                                                        // Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type
        }
    }
}