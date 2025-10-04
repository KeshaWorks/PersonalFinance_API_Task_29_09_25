using Project.Interfaces;
using Project.Models;

namespace Project.Services
{
    /// <summary>
    /// Service that manages recommendations
    /// </summary>
    public class RecommendationManagerService : IRecommendationManagerService
    {
        private readonly ILogger<RecommendationManagerService> _logger;

        public RecommendationManagerService(ILogger<RecommendationManagerService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Create 3 or less recommendations about finance
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="users"></param>
        /// <returns>Recomendation list</returns>
        public InsightSaving[] GetInsightsSavings(int userId, List<User> users)
        {
            if (!users.Any(x => x.UserId == userId))
            {
                _logger.LogError("Пользователь не найден");
                throw new Exception("Такого пользователя не существует");
            }

            User user = users.Find(x => x.UserId == userId);
            int userCatigoriesCount = user.Categories.Count;
            List<UsersManagerServiceHelper> usersManagerRepositorieHelpers = new List<UsersManagerServiceHelper>();

            for (int i = 0; i < userCatigoriesCount; i++)
            {
                decimal totalSum = 0;

                for (int j = 0; j < userCatigoriesCount; j++)
                {
                    // Calculate common sum of every transactions for every category
                    totalSum += user.Categories[i].Transactions[j].Amount;
                }

                usersManagerRepositorieHelpers.Add(new UsersManagerServiceHelper(user.Categories[i].Limit, totalSum, user.Categories[i].CategoryName));
            }

            // Searching for 3 the most filled in categories
            usersManagerRepositorieHelpers = usersManagerRepositorieHelpers
                .Where(x => (x.TotalSum / x.Limit) * 100 > 80)
                .OrderBy(x => x.TotalSum)
                .Take(3)
                .ToList();
            InsightSaving[] insightSavings = new InsightSaving[3];

            for (int i = 0; i < usersManagerRepositorieHelpers.Count; i++)
            {
                decimal limit = usersManagerRepositorieHelpers[i].Limit;
                decimal totalSum = usersManagerRepositorieHelpers[i].TotalSum;

                // Filling recommendation list
                insightSavings[i] = new InsightSaving($"«Вы использовали {Math.Round((totalSum / limit) * 100)}% бюджета в категории \"{{{usersManagerRepositorieHelpers[i].Categorie}}}\". " +
                    $"Рассмотрите возможность сократить расходы.»", totalSum / 2, usersManagerRepositorieHelpers[i].Categorie);
            }

            return insightSavings;
        }
    }
}