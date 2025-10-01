using Project.Interfaces;
using Project.Models;

namespace Project.Services
{
    public class LimitManagerService : ILimitManagerService
    {
        private IUserManagerRepositorie _userManagerRepositorie;

        public LimitManagerService(IUserManagerRepositorie userManagerRepositorie)
        {
            _userManagerRepositorie = userManagerRepositorie;
        }

        public void AddLimit(AddLimitRequest addLimitRequest)
        {
            if (CheckUserIdValidation(addLimitRequest.UserId))
            {
                throw new Exception("Такого пользователя не существует");
            }
            User user = _userManagerRepositorie.Users.Find(x => x.UserId == addLimitRequest.UserId);
            if (!user.Categories.Any(x => x.CategoryName == addLimitRequest.СategoryName))
            {
                user.Categories.Add(new Category
                {
                    CategoryName = addLimitRequest.СategoryName,
                    Limit = addLimitRequest.Limit
                });
            }
            Category category = user.Categories.Find(x => x.CategoryName == addLimitRequest.СategoryName);
            category.Limit += addLimitRequest.Limit;
        }

        private bool CheckUserIdValidation(int userId) => !_userManagerRepositorie.Users.Any(x => x.UserId == userId);
    }
}