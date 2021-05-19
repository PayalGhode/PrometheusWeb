using PrometheusWeb.Data.UserModels;
using System.Linq;
using System.Web.Http;

namespace PrometheusWeb.Services.Services
{
    public interface IStudentService
    {
        StudentUserModel DeleteStudent(int id);
        StudentUserModel GetStudent(int id);
        IQueryable<StudentUserModel> GetStudents();
        bool AddStudent(StudentUserModel studentModel);
        bool UpdateStudent(int id, StudentUserModel studentModel);
        bool IsStudentExists(int id);
        int GetStudentID(string UserID);
    }
}