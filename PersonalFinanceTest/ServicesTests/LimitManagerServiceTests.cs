using Moq;
using Project.Interfaces;
using Microsoft.Extensions.Logging;
using Project.Models.TakedFromBody;
using Project.Services;
using Project.Models;

namespace PersonalFinanceTest.ServicesTests
{
    public class LimitManagerServiceTests
    {
        [Fact]
        public void AddLimitTest()
        {
            var repositorieMock = new Mock<IUserManagerRepositorie>();
            var loggerMock = new Mock<ILogger<LimitManagerService>>();

            List<Category> categories =
            [
                new Category{ CategoryName = "Test", Limit = 100, Transactions = new List<Transaction>()}
            ];

            User user = new User { UserId = 1, Categories = categories };

            repositorieMock.Setup(x => x.GetUserById(1))
                    .Returns(user);

            LimitManagerService limitManagerService = new LimitManagerService(repositorieMock.Object, loggerMock.Object);

            limitManagerService.AddLimit(new AddLimitRequest { Limit = 1000, UserId = 1, СategoryName = "Test"});

            var result = repositorieMock.Object.GetUserById(1).Categories.Find(x => x.CategoryName == "Test").Limit;

            Assert.Equal(1000, result);
            Assert.Equal(1, repositorieMock.Object.GetUserById(1).Categories.Count());
        }
    }
}
