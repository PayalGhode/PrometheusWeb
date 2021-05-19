using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrometheusWeb.Services;
using PrometheusWeb.Services.Services;
using PrometheusWeb.Data.UserModels;

namespace PrometheusWeb.UnitTest.Services
{
    [TestClass]
    public class AssignmentServiceTests
    {
        AssignmentService assignmentService = new AssignmentService();


        [TestMethod]
        public void GetAssignmentsTest()
        {

            //Act
            var result = assignmentService.GetAssignments();

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetAssignmentTest()
        {
            int Id = 10;
            //Arrange 

            //Act
            var result = assignmentService.GetAssignment(Id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddAssignmentTest()
        {
            //Arrange
            AssignmentUserModel assignmentUserModel = new AssignmentUserModel
            {
                AssignmentID = 99,
                HomeWorkID = 1,
                CourseID = 2,
                TeacherID = 1


            };
            var result = assignmentService.AddAssignment(assignmentUserModel);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateAssignmentTest()
        {
            //Arrange
            int id = 10;
            AssignmentUserModel assignmentUserModel = new AssignmentUserModel
            {
                AssignmentID = 10,
                HomeWorkID = 10,
                CourseID = 1,
                TeacherID = 1


            };
            var result = assignmentService.UpdateAssignment(id, assignmentUserModel);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void DeleteAssignmentTest()
        {
            //Arrange
            int id = 10;
            //Act

            var result = assignmentService.DeleteAssignment(id);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IsAssignmentExistsTest()
        {
            //Arrange
            int id = 11;
            //Act

            var result = assignmentService.IsAssignmentExists(id);
            //Assert
            Assert.IsTrue(result);
        }
    }
}
