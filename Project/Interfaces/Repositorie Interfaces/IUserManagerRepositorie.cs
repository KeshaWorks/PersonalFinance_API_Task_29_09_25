using Project.Models;

namespace Project.Interfaces
{
    public interface IUserManagerRepositorie
    {
        public List<User> GetUsers();
        public User GetUserById(int userId);
    }
}