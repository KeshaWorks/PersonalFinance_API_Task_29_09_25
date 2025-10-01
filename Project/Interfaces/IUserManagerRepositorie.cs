using Project.Models;

namespace Project.Interfaces
{
    public interface IUserManagerRepositorie
    {
        public List<User> Users { get; set; }
    }
}