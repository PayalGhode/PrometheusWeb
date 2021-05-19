using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Data.UserModels;
using System.Linq;
using System.Web.Http;

namespace PrometheusWeb.Services.Services
{
    public interface ITeachesService
    {
        TeacherCourseUserModel DeleteTeacherCourse(int id);
        IQueryable<TeacherCourseUserModel> GetTeacherCourses();
        TeacherCourseUserModel GetTeacherCourses(int id);
        bool AddTeacherCourse(TeacherCourseUserModel teacherCourseModel);
        bool UpdateTeacherCourses(int id, TeacherCourseUserModel teacherCourseModel);
        bool IsTeachesExists(int id);

    }
}