using Project.Models;

namespace Project.Interfaces
{
    public interface IUserManagerRepositorie
    {
        public User GetUserById(int userId);
    }
}