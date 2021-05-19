using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Data.UserModels;
using System.Linq;
using System.Web.Http;

namespace PrometheusWeb.Services.Services
{
    public interface IAssignmentService
    {
        AssignmentUserModel DeleteAssignment(int id);
        AssignmentUserModel GetAssignment(int id);
        IQueryable<AssignmentUserModel> GetAssignments();
        bool AddAssignment(AssignmentUserModel userModel);
        bool UpdateAssignment(int id, AssignmentUserModel userModel);
        bool IsAssignmentExists(int id);
    }
}