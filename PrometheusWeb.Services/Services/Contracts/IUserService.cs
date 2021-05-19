using PrometheusWeb.Data.UserModels;
using System.Linq;
using System.Web.Http;

namespace PrometheusWeb.Services.Services
{
    public interface IUserService
    {
        AdminUserModel DeleteUser(string id);
        AdminUserModel GetUser(string id);
        IQueryable<AdminUserModel> GetUsers();
        bool AddUser(AdminUserModel adminModel);
        bool UpdateUser(string id, AdminUserModel adminModel);
        bool IsUserExists(string id);
    }
}