using Project.Interfaces;
using Project.Models;

namespace Project.Repositories
{
    public class UsersManagerRepositorie : IUsersManagerRepositorie
    {
        private static int _id;
        public List<User> Users { get; set; } = new List<User>();
    }
}