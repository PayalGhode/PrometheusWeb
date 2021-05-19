using PrometheusWeb.Data.DataModels;
using PrometheusWeb.Data.UserModels;
using System.Linq;
using System.Web.Http;

namespace PrometheusWeb.Services.Services
{
    public interface IHomeworkService
    {
        HomeworkUserModel DeleteHomework(int id);
        HomeworkUserModel GetHomework(int id);
        IQueryable<HomeworkUserModel> GetHomeworks();
        bool AddHomework(HomeworkUserModel homeworkModel);
        bool UpdateHomework(int id, HomeworkUserModel homeworkModel);
        bool IsHomeworkExists(int id);
    }
}