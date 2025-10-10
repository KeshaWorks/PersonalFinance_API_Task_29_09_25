using Project.Repositories;
using Microsoft.Extensions.Logging;
using Project.Models;
using Moq;

namespace PersonalFinanceTest.RepositoriesTests
{
    public class UserManagerRepositorieTests
    {
        [Fact]
        public void Get_user_when_id_unvalid()
        {
            var logger = new Mock<ILogger<UserManagerRepositorie>>();

            var userManagerRepositorie = new UserManagerRepositorie(logger.Object);

            userManagerRepositorie.Users = 
            [
                new User { UserId = 1, Categories = new List<Category>()},
                new User { UserId = 2, Categories = new List<Category>()},
                new User { UserId = 3, Categories = new List<Category>()},
            ];

            var ex = Assert.Throws<Exception>(() => userManagerRepositorie.GetUserById(4)); 

            Assert.Equal("Такого пользователя не существует", ex.Message);
        }
    }
}