using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrometheusWeb.Data.UserModels;
using PrometheusWeb.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrometheusWeb.Services.Services.Tests
{
    [TestClass()]
    public class EnrollmentServiceTests
    {
        [TestMethod()]
        public void GetEnrollmentTest()
        {
            //Arrange
            EnrollmentService enrollmentService = new EnrollmentService();
            int id = 2;

            //Act
            var result = enrollmentService.GetEnrollment(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetEnrollmentsTest()
        {
            //Arrange
            EnrollmentService enrollmentService = new EnrollmentService();

            //Act
            var result = enrollmentService.GetEnrollments();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void AddEnrollmentTest()
        {
            //Arrange
            EnrollmentService enrollmentService = new EnrollmentService();
            EnrollmentUserModel userModel = new EnrollmentUserModel
            {
                EnrollmentID = 99,
                CourseID = 2,
                StudentID = 1
            };

            //Act
            var result = enrollmentService.AddEnrollment(userModel);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void UpdateEnrollmentTest()
        {
            //Arrange
            EnrollmentService enrollmentService = new EnrollmentService();
            EnrollmentUserModel userModel = new EnrollmentUserModel
            {
                EnrollmentID = 5,
                CourseID = 2,
                StudentID = 1
            };

            //Act
            var result = enrollmentService.UpdateEnrollment(5, userModel);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteEnrollmentTest()
        {
            //Arrange
            EnrollmentService enrollmentService = new EnrollmentService();
            int id = 5;

            //Act
            var result = enrollmentService.DeleteEnrollment(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void EnrollmentExistsTest()
        {
            //Arrange
            EnrollmentService enrollmentService = new EnrollmentService();
            int id = 2;

            //Act
            var result = enrollmentService.IsEnrollmentExists(id);

            //Assert
            Assert.IsTrue(result);
        }
    }
}