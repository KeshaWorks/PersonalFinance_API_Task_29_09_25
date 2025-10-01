using Project.Interfaces;
using Project.Models;

namespace Project.Repositories
{
    public class UsersManagerRepositorie : IUserManagerRepositorie
    {
        public List<User> Users { get; set; } =
        [
            new User { UserId = 1, Finance = 1000, Categories = new List<Category>()},
            new User { UserId = 2, Finance = 5000, Categories = new List<Category>()},
            new User { UserId = 3, Finance = 250, Categories = new List<Category>()},
        ]; 
    }
}