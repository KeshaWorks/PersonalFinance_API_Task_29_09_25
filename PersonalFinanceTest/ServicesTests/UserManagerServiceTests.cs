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

            userManagerService.AddTransaction(new AddTransactionRequest { Amount = 500, Description = "Test", UserId = 1, СategoryName = "Test" });

            Assert.Equal(categories, repositorieMock.Object.GetUserById(1).Categories);
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

        [Fact]
        public void Add_transaction_when_categorie_dont_exist_test()
        {
            var repositorieMock = new Mock<IUserManagerRepositorie>();
            var loggerMock = new Mock<ILogger<UserManagerService>>();

            List<Category> categories =
            [
                new Category{ CategoryName = "Test1", Limit = 1000, Transactions = new List<Transaction>()}
            ];

            User user = new User { UserId = 1, Categories = categories };

            repositorieMock.Setup(x => x.GetUserById(1))
                    .Returns(user);

            UserManagerService userManagerService = new UserManagerService(repositorieMock.Object, loggerMock.Object);

            var result = Record.Exception(() => userManagerService.AddTransaction(new AddTransactionRequest { Amount = 500, Description = "Test2", UserId = 1, СategoryName = "Test" }));

            Assert.Equal("Сначала установаите лимит для этой категории!", result.Message);
        }

        [Fact]
        public void Add_transaction_when_amount_more_limit()
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

            var result = Record.Exception(() => userManagerService.AddTransaction(new AddTransactionRequest { Amount = 1100, Description = "Test2", UserId = 1, СategoryName = "Test" }));

            Assert.Equal("Вы не можете превысить лимит по этой категории!", result.Message);
        }

        [Fact]
        public void Add_transaction_in_certain_categorie()
        {
            var repositorieMock = new Mock<IUserManagerRepositorie>();
            var loggerMock = new Mock<ILogger<UserManagerService>>();

            List<Category> categories =
            [
                new Category{ CategoryName = "Test1", Limit = 1000, Transactions = new List<Transaction>()},
                new Category{ CategoryName = "Test2", Limit = 1000, Transactions = new List<Transaction>()}
            ];

            User user = new User { UserId = 1, Categories = categories };

            repositorieMock.Setup(x => x.GetUserById(1))
                    .Returns(user);

            UserManagerService userManagerService = new UserManagerService(repositorieMock.Object, loggerMock.Object);

            userManagerService.AddTransaction(new AddTransactionRequest { Amount = 500, Description = "Test1", UserId = 1, СategoryName = "Test1" });

            Assert.Equal(categories.Find(x => x.CategoryName == "Test2").Transactions, repositorieMock.Object.GetUserById(1).Categories.Find(x => x.CategoryName == "Test2").Transactions);
        }

        [Fact]
        public void Get_analyzes_for_some_categories()
        {
            var repositorieMock = new Mock<IUserManagerRepositorie>();
            var loggerMock = new Mock<ILogger<UserManagerService>>();

            List<Category> categories =
            [
                new Category{ CategoryName = "Categorie1", Limit = 1000, Transactions = 
                [
                    new Transaction("Transaction1", 500), 
                    new Transaction("Transaction2", 300)
                ]},
                new Category{ CategoryName = "Categorie2", Limit = 900, Transactions = [new Transaction("Transaction2", 400)]},
               
            ];

            User user = new User { UserId = 1, Categories = categories };

            repositorieMock.Setup(x => x.GetUserById(1))
                    .Returns(user);

            UserManagerService userManagerService = new UserManagerService(repositorieMock.Object, loggerMock.Object);

            List<Analysis> result = userManagerService.GetAnalyzes(1);

            List<Analysis> expectedResult =
            [
                new Analysis("Categorie1", 1000, 800),
                new Analysis("Categorie2", 900, 400),
            ];

            Assert.Equal(expectedResult, result);
        }
    }
}