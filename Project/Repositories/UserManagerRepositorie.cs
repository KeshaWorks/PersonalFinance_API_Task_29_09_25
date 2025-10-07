using Project.Interfaces;
using Project.Models;

namespace Project.Repositories
{
    /// <summary>
    /// Repositorie that keeps and provides Users list
    /// </summary>
    public class UserManagerRepositorie : IUserManagerRepositorie
    {
        private ILogger<UserManagerRepositorie> _logger;

        public UserManagerRepositorie(ILogger<UserManagerRepositorie> logger)
        {
            _logger = logger;
        }

        public List<User> Users { get; set; } =
        [
            new User { UserId = 1, Categories = new List<Category>()},
            new User { UserId = 2, Categories = new List<Category>()},
            new User { UserId = 3, Categories = new List<Category>()},
        ];

        public List<User> GetUsers()
        {
            return Users;
        }

        public User GetUserById(int userId)
        {
            if (!Users.Any(x => x.UserId == userId))
            {
                _logger.LogError("Пользователь не найден");
                throw new Exception("Такого пользователя не существует");
            }
            return Users.Find(x => x.UserId == userId);
        }
    }
}