using Project.Interfaces;
using Project.Models;

namespace Project.Services
{
    /// <summary>
    /// Service that manages limit
    /// </summary>
    public class LimitManagerService : ILimitManagerService
    {
        private readonly ILogger<LimitManagerService> _logger;

        public LimitManagerService(ILogger<LimitManagerService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Set limit 
        /// </summary>
        /// <param name="addLimitRequest"></param>
        /// <param name="users"></param>
        public void AddLimit(AddLimitRequest addLimitRequest, List<User> users)
        {
            if (!users.Any(x => x.UserId == addLimitRequest.UserId))
            {
                _logger.LogError("Пользователь не найден");
                throw new Exception("Такого пользователя не существует");
            }

            User user = users.Find(x => x.UserId == addLimitRequest.UserId);

            // Check Id this category exist otherwise set new limit
            if (!user.Categories.Any(x => x.CategoryName == addLimitRequest.СategoryName))
            {
                user.Categories.Add(new Category
                {
                    CategoryName = addLimitRequest.СategoryName,
                    Limit = addLimitRequest.Limit
                });
            }
            else
            {
                Category category = user.Categories.Find(x => x.CategoryName == addLimitRequest.СategoryName);
                category.Limit = addLimitRequest.Limit;
            }
        }
    }
}