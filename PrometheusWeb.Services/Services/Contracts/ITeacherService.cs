using PrometheusWeb.Data.UserModels;
using System.Linq;
using System.Web.Http;

namespace PrometheusWeb.Services.Services
{
    public interface ITeacherService
    {
        TeacherUserModel DeleteTeacher(int id);
        TeacherUserModel GetTeacher(int id);
        IQueryable<TeacherUserModel> GetTeachers();
        bool AddTeacher(TeacherUserModel teacherModel);
        bool UpdateTeacher(int id, TeacherUserModel teacherModel);
        bool IsTeacherExists(int id);
        int GetTeacherID(string UserID);
    }
}