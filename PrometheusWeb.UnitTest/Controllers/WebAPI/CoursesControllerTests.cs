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

namespace PrometheusWeb.Services.Controllers.Tests
{
    [TestClass()]
    public class CoursesControllerTests
    {
        public readonly ICourseService MockProductsRepository;
        public CoursesControllerTests()
        {
            // create some mock Items to play with
            IQueryable<CourseUserModel> enrollments = new List<CourseUserModel>
                {
                    new CourseUserModel
                    {
                        CourseID = 1,
                        Name = "Math",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now
                    },
                    new CourseUserModel
                    {
                        CourseID = 2,
                        Name = "Science",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now
                    }
                }.AsQueryable();
            // Mock the Course Repository using Moq
            Mock<ICourseService> mockProductRepository = new Mock<ICourseService>();

            // Return all the items
            mockProductRepository.Setup(mr => mr.GetCourses()).Returns(enrollments);

            // return a item by Id
            mockProductRepository.Setup(mr => mr.GetCourse(
                It.IsAny<int>())).Returns((int i) => enrollments.Where(
                x => x.CourseID == i).Single());

            // Add a item   
            mockProductRepository.Setup(mr => mr.AddCourse(
                It.IsAny<CourseUserModel>())).Returns((CourseUserModel target) => {
                    target.CourseID = enrollments.Count() + 1;
                    enrollments.ToList().Add(target);
                    return true;
                });
            // Update a item   
            mockProductRepository.Setup(mr => mr.UpdateCourse(It.IsAny<int>(), It.IsAny<CourseUserModel>())).Returns((int id, CourseUserModel target) => {
                var original = enrollments.Where(
                        q => q.CourseID == target.CourseID).Single();

                if (original == null)
                {
                    return false;
                }

                original.CourseID = target.CourseID;
                original.Name = target.Name;
                original.StartDate = target.StartDate;
                original.EndDate = target.EndDate;

                return true;
            });

            // Delete a item   
            mockProductRepository.Setup(mr => mr.DeleteCourse(
                It.IsAny<int>())).Returns((int id) => {
                    var original = enrollments.Where(
                            q => q.CourseID == id).Single();
                    enrollments.ToList().Remove(original);
                    return original;
                });

            // Complete the setup of our Mock Course Repository
            this.MockProductsRepository = mockProductRepository.Object;
        }

        [TestMethod()]
        public void GetCoursesTest()
        {    
            //Arrange
            CoursesController coursesController = new CoursesController(this.MockProductsRepository);
            //Act
            var result = coursesController.GetCourses();
            //Assert
            Assert.IsNotNull(result); // Test if null
           
        }

        [TestMethod()]
        public void GetCourseTest()
        {

            //Arrange
            CoursesController coursesController = new CoursesController(this.MockProductsRepository);
            int id = 1;
            //Act
            var result = coursesController.GetCourse(id);
            //Assert
            Assert.IsNotNull(result); // Test if null   
        }

           
        [TestMethod()]
        public void PutCourseTest()
        {
            //Arrange
            CoursesController coursesController = new CoursesController(this.MockProductsRepository);
            CourseUserModel courseUserModel = new CourseUserModel();
            int id = 1;
            //Act
            var result = coursesController.PutCourse(id,courseUserModel);
            //Assert
            Assert.IsNotNull(result); // Test if null   
        }

            [TestMethod()]
        public void PostCourseTest()
        {
            //Arrange
            CoursesController coursesController = new CoursesController(this.MockProductsRepository);
            CourseUserModel courseUserModel = new CourseUserModel();
            int id = 1;
            //Act
            var result = coursesController.PostCourse( courseUserModel);
            //Assert
            Assert.IsNotNull(result); // Test if null   

        }

        [TestMethod()]
        public void DeleteCourseTest()
        {

            //Arrange
            CoursesController coursesController = new CoursesController(this.MockProductsRepository);
            CourseUserModel courseUserModel = new CourseUserModel();
            int id = 1;
            //Act
            var result = coursesController.DeleteCourse(id);
            //Assert
            Assert.IsNotNull(result); // Test if null   


        }
    }
}