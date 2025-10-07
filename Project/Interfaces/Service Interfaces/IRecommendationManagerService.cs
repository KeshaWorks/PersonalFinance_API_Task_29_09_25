using Project.Models;

namespace Project.Interfaces
{
    public interface IRecommendationManagerService
    {
        public InsightSaving[] GetInsightsSavings(int userId);
    }
}