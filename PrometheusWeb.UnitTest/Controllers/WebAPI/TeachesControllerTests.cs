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
    public class TeachesControllerTests
    {
        public readonly ITeachesService MockProductsRepository;
        public TeachesControllerTests()
        {

            // create some mock Items to play with
            IQueryable<TeacherCourseUserModel> enrollments = new List<TeacherCourseUserModel>
                {
                    new TeacherCourseUserModel
                    {
                        TeacherCourseID = 1,
                        TeacherID = 1,
                        CourseID = 1
                    },
                    new TeacherCourseUserModel
                    {
                        TeacherCourseID = 2,
                        TeacherID = 1,
                        CourseID = 2
                    }
                }.AsQueryable();
            // Mock the Teaches Repository using Moq
            Mock<ITeachesService> mockProductRepository = new Mock<ITeachesService>();

            // Return all the items
            mockProductRepository.Setup(mr => mr.GetTeacherCourses()).Returns(enrollments);

            // return a item by Id
            mockProductRepository.Setup(mr => mr.GetTeacherCourses(
                It.IsAny<int>())).Returns((int i) => enrollments.Where(
                x => x.TeacherCourseID == i).Single());

            // Add a item   
            mockProductRepository.Setup(mr => mr.AddTeacherCourse(
                It.IsAny<TeacherCourseUserModel>())).Returns((TeacherCourseUserModel target) => {
                    target.TeacherCourseID = enrollments.Count() + 1;
                    enrollments.ToList().Add(target);
                    return true;
                });
            // Update a item   
            mockProductRepository.Setup(mr => mr.UpdateTeacherCourses(It.IsAny<int>(), It.IsAny<TeacherCourseUserModel>())).Returns((int id, TeacherCourseUserModel target) => {
                var original = enrollments.Where(
                        q => q.TeacherCourseID == target.TeacherCourseID).Single();

                if (original == null)
                {
                    return false;
                }

                original.TeacherCourseID = target.TeacherCourseID;
                original.CourseID = target.CourseID;
                original.TeacherID = target.TeacherID;

                return true;
            });

            // Delete a item   
            mockProductRepository.Setup(mr => mr.DeleteTeacherCourse(
                It.IsAny<int>())).Returns((int id) => {
                    var original = enrollments.Where(
                            q => q.TeacherCourseID == id).Single();
                    enrollments.ToList().Remove(original);
                    return original;
                });

            // Complete the setup of our Mock TeacherCourse Repository
            this.MockProductsRepository = mockProductRepository.Object;
        }

        [TestMethod()]
        public void GetTeacherCoursesTest()
        {
            //Arrange
            TeachesController controller = new TeachesController(this.MockProductsRepository);

            //Act
            var result = controller.GetTeacherCourses();

            //Assert
            Assert.IsNotNull(result); // Test if null
            Assert.IsInstanceOfType(result, typeof(IQueryable<TeacherCourseUserModel>)); // Test type
        }

        [TestMethod()]
        public void GetTeacherCoursesTest1()
        {
            //Arrange
            TeachesController controller = new TeachesController(this.MockProductsRepository);
            int id = 1;

            //Act
            var result = controller.GetTeacherCourses(id);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
                                                                        // Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type
        }

        [TestMethod()]
        public void PutTeacherCoursesTest()
        {
            //Arrange
            TeachesController controller = new TeachesController(this.MockProductsRepository);
            int id = 1;
            TeacherCourseUserModel userModel = new TeacherCourseUserModel
            {
                TeacherCourseID = id,
                CourseID = 1,
                TeacherID = 1
            };
            //Act
            var result = controller.PutTeacherCourses(id,userModel);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
            Assert.IsInstanceOfType(result, typeof(OkResult)); // Test type
        }

        [TestMethod()]
        public void PostTeacherCourseTest()
        {
            //Arrange
            TeachesController controller = new TeachesController(this.MockProductsRepository);
            int id = 1;
            TeacherCourseUserModel teacherCourseUserModel = new TeacherCourseUserModel
            {
                TeacherCourseID = id,
                CourseID = 1,
                TeacherID = 1
            };
            //Act
            var result = controller.PostTeacherCourse(teacherCourseUserModel);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
        }

        [TestMethod()]
        public void DeleteTeacherCourseTest()
        {
            //Arrange
            TeachesController controller = new TeachesController(this.MockProductsRepository);
            int id = 1;

            //Act
            var result = controller.DeleteTeacherCourse(id);

            //Assert
            Assert.IsNotNull(result); // Test if null

            Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type of returned object
                                                                        // Assert.IsInstanceOfType(result, typeof(IHttpActionResult)); // Test type

        }
    }
}