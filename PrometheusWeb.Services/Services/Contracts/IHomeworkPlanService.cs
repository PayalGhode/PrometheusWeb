using PrometheusWeb.Data.DataModels;
using System.Linq;
using System.Web.Http;
using PrometheusWeb.Data.UserModels;

namespace PrometheusWeb.Services.Services
{
    public interface IHomeworkPlanService
    {
        HomeworkPlanUserModel DeleteHomeworkPlan(int id);
        bool DeleteHomeworkPlans(int StudentID);
        HomeworkPlanUserModel GetHomeworkPlan(int id);
        IQueryable<HomeworkPlanUserModel> GetHomeworkPlans(int StudentID);
        IQueryable<HomeworkPlanUserModel> GetHomeworkPlans();
        bool AddHomeworkPlan(HomeworkPlanUserModel homeworkPlanModel);
        bool AddHomeworkPlans(IQueryable<HomeworkPlanUserModel> homeworkPlanModels);
        bool UpdateHomeworkPlan(int id, HomeworkPlanUserModel homeworkPlanModel);
        bool UpdateHomeworkPlans(IQueryable<HomeworkPlanUserModel> homeworkPlanModels);
        bool IsHomeworkPlanExists(int id);
    }
}