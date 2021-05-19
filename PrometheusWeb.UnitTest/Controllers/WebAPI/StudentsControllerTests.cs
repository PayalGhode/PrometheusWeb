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
    public class StudentsControllerTests
    {
        public readonly IStudentService MockProductsRepository;
        public StudentsControllerTests()
        {

            // create some mock items to play with
            IQueryable<StudentUserModel> enrollments = new List<StudentUserModel>
                {
                    new StudentUserModel
                    {
                        StudentID = 1,
                        FName = "Ram",
                        LName = "Sharma",
                        DOB = DateTime.Now,
                        UserID = "New1919",
                        Address  = "XYZ",
                        City = "LV",
                        MobileNo = "0000000000"
                    },
                    new StudentUserModel
                    {
                        StudentID = 2,
                        FName = "Shyam",
                        LName = "Verma",
                        DOB = DateTime.Now,
                        UserID = "New2021",
                        Address  = "XYZ2",
                        City = "LV1",
                        MobileNo = "1111111111"
                    }
                }.AsQueryable();
            // Mock the Student Repository using Moq
            Mock<IStudentService> mockProductRepository = new Mock<IStudentService>();

            // Return all the items
            mockProductRepository.Setup(mr => mr.GetStudents()).Returns(enrollments);

            // return a item by Id
            mockProductRepository.Setup(mr => mr.GetStudent(
                It.IsAny<int>())).Returns((int i) => enrollments.Where(
                x => x.StudentID == i).Single());

            // Add a item   
            mockProductRepository.Setup(mr => mr.AddStudent(
                It.IsAny<StudentUserModel>())).Returns((StudentUserModel target) => {
                    target.StudentID = enrollments.Count() + 1;
                    enrollments.ToList().Add(target);
                    return true;
                });
            // Update a item   
            mockProductRepository.Setup(mr => mr.UpdateStudent(It.IsAny<int>(), It.IsAny<StudentUserModel>())).Returns((int id, StudentUserModel target) => {
                var original = enrollments.Where(
                        q => q.StudentID == target.StudentID).Single();

                if (original == null)
                {
                    return false;
                }

                original.StudentID = target.StudentID;
                original.FName = target.FName;
                original.LName = target.LName;
                original.DOB = target.DOB;
                original.Address = target.Address;
                original.City = target.City;
                original.UserID = target.UserID;
                original.MobileNo = target.MobileNo;

                return true;
            });

            // Delete a item   
            mockProductRepository.Setup(mr => mr.DeleteStudent(
                It.IsAny<int>())).Returns((int id) => {
                    var original = enrollments.Where(
                            q => q.StudentID == id).Single();
                    enrollments.ToList().Remove(original);
                    return original;
                });

            // Complete the setup of our Mock Student Repository
            this.MockProductsRepository = mockProductRepository.Object;
        }

        [TestMethod()]
        public void GetStudentsTest()
        {
            //Arrange
            StudentsController controller = new StudentsController(this.MockProductsRepository);

            //Act
            var result = controller.GetStudents();

            //Assert
            Assert.IsNotNull(result); // Test if null
            Assert.IsInstanceOfType(result, typeof(IQueryable<EnrollmentUserModel>)); // Test type
        }

        [TestMethod()]
        public void GetStudentTest()
        {
            //Arrange
            StudentsController controller = new StudentsController(this.MockProductsRepository);
            int id = 1;

            //Act
            var result = controller.GetStudent(id);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
                                                                        // Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type
        }

        [TestMethod()]
        public void PutStudentTest()
        {
            //Arrange
            StudentsController controller = new StudentsController(this.MockProductsRepository);
            int id = 1;
            StudentUserModel userModel = new StudentUserModel
            {
                StudentID = 1,
                FName = "Ram",
                LName = "Sharma",
                DOB = DateTime.Now,
                UserID = "New1919",
                Address = "XYZ",
                City = "LV",
                MobileNo = "0000000000"
            };
            //Act
            var result = controller.PutStudent(id, userModel);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
            Assert.IsInstanceOfType(result, typeof(OkResult)); // Test type
        }

        [TestMethod()]
        public void PostStudentTest()
        {
            //Arrange
            StudentsController controller = new StudentsController(this.MockProductsRepository);
            int id = 1;
            StudentUserModel userModel = new StudentUserModel
            {
                StudentID = 1,
                FName = "Ram",
                LName = "Sharma",
                DOB = DateTime.Now,
                UserID = "New1919",
                Address = "XYZ",
                City = "LV",
                MobileNo = "0000000000"
            };
            //Act
            var result = controller.PostStudent(userModel);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
        }

        [TestMethod()]
        public void DeleteStudentTest()
        {
            //Arrange
            StudentsController controller = new StudentsController(this.MockProductsRepository);
            int id = 1;

            //Act
            var result = controller.DeleteStudent(id);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object

        }
    }
}