using Project.Interfaces;
using Project.Models;

namespace Project.Services
{
    /// <summary>
    /// Service that manages limit
    /// </summary>
    public class LimitManagerService : ILimitManagerService
    {
        /// <summary>
        /// Set limit 
        /// </summary>
        /// <param name="addLimitRequest"></param>
        /// <param name="users"></param>
        public void AddLimit(AddLimitRequest addLimitRequest, List<User> users)
        {
            User user = users.Find(x => x.UserId == addLimitRequest.UserId);

            //Check Id this category exist otherwise set new limit
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