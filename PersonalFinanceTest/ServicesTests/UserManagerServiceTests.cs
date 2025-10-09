using Moq;
using Project.Interfaces;
using Microsoft.Extensions.Logging;
using Project.Models.TakedFromBody;
using Project.Services;
using Project.Models;

namespace PersonalFinanceTest.ServicesTests
{
    public class UserManagerServiceTests
    {
        [Fact]
        public void AddTransactionTest()
        {
            var repositorieMock = new Mock<IUserManagerRepositorie>();
            var loggerMock = new Mock<ILogger<UserManagerService>>();

            List<Category> categories =
            [
                new Category{ CategoryName = "Test", Limit = 1000, Transactions = new List<Transaction>()}
            ];

            User user = new User { UserId = 1, Categories = categories };

            repositorieMock.Setup(x => x.GetUserById(1))
                    .Returns(user);

            UserManagerService userManagerService = new UserManagerService(repositorieMock.Object, loggerMock.Object);

            var ex = Record.Exception(() => userManagerService.AddTransaction(new AddTransactionRequest { Amount = 500, Description = "Test", UserId = 1, СategoryName = "Test" }));

            Assert.Null(ex);
        }

        [Fact]
        public void GetUserTransactionsTest()
        {
            var repositorieMock = new Mock<IUserManagerRepositorie>();
            var loggerMock = new Mock<ILogger<UserManagerService>>();

            List<Category> categories =
            [
                new Category{ CategoryName = "Test", Limit = 1000, Transactions = [new Transaction("Test", 500)]}
            ];

            User user = new User { UserId = 1, Categories = categories };

            repositorieMock.Setup(x => x.GetUserById(1))
                    .Returns(user);

            UserManagerService userManagerService = new UserManagerService(repositorieMock.Object, loggerMock.Object);

            List<Transaction> result = userManagerService.GetUserTransactions(1);

            List<Transaction> expectedResult =
            [
                new Transaction("Test", 500)
            ];

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GetAnalyzes()
        {
            var repositorieMock = new Mock<IUserManagerRepositorie>();
            var loggerMock = new Mock<ILogger<UserManagerService>>();

            List<Category> categories =
            [
                new Category{ CategoryName = "Test", Limit = 1000, Transactions = [new Transaction("Test", 500)]}
            ];

            User user = new User { UserId = 1, Categories = categories };

            repositorieMock.Setup(x => x.GetUserById(1))
                    .Returns(user);

            UserManagerService userManagerService = new UserManagerService(repositorieMock.Object, loggerMock.Object);

            List<Analysis> result = userManagerService.GetAnalyzes(1);

            List<Analysis> expectedResult =
            [
                new Analysis("Test", 1000, 500)
            ];

            Assert.Equal(expectedResult, result);
        }
    }
}