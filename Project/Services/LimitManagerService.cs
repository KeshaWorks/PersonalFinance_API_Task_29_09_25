using Project.Interfaces;
using Project.Models;
using Project.Models.TakedFromBody;

namespace Project.Services
{
    /// <summary>
    /// Service that manages limit
    /// </summary>
    public class LimitManagerService : ILimitManagerService
    {
        private IUserManagerRepositorie _userManagerRepositorie;

        private readonly ILogger<LimitManagerService> _logger;

        public LimitManagerService(IUserManagerRepositorie userManagerRepositorie, ILogger<LimitManagerService> logger)
        {
            _userManagerRepositorie = userManagerRepositorie;
            _logger = logger;
        }

        /// <summary>
        /// Set limit 
        /// </summary>
        /// <param name="addLimitRequest"></param>
        /// <param name="users"></param>
        public void AddLimit(AddLimitRequest addLimitRequest)
        {
            var user = _userManagerRepositorie.GetUserById(addLimitRequest.UserId);

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

            _logger.LogInformation($"Лимит добавлен для {addLimitRequest.СategoryName}");
        }
    }
}