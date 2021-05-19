using PrometheusWeb.Data.UserModels;
using System.Linq;
using System.Web.Http;

namespace PrometheusWeb.Services.Services
{
    public interface ICourseService
    {
        CourseUserModel DeleteCourse(int id);
        CourseUserModel GetCourse(int id);
        IQueryable<CourseUserModel> GetCourses();
        bool AddCourse(CourseUserModel courseModel);
        bool UpdateCourse(int id, CourseUserModel courseModel);
        bool IsCourseExists(int id);
    }
}