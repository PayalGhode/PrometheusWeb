using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Data.UserModels;
using System.Linq;
using System.Web.Http;

namespace PrometheusWeb.Services.Services
{
    public interface IEnrollmentService
    {
        EnrollmentUserModel DeleteEnrollment(int id);
        EnrollmentUserModel GetEnrollment(int id);
        IQueryable<EnrollmentUserModel> GetEnrollments();
        bool AddEnrollment(EnrollmentUserModel enrollmentModel);
        bool UpdateEnrollment(int id, EnrollmentUserModel enrollmentModel);
        bool IsEnrollmentExists(int id);
    }
}