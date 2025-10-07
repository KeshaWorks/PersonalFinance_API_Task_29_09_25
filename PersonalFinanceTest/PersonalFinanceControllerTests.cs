using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Project.Controllers;
using Project.Interfaces;
using Project.Models;
using Project.Models.TakedFromBody; 
using Transaction = Project.Models.Transaction;

namespace PersonalFinanceTest
{
    public class PersonalFinanceControllerTests
    {
        [Fact]
        public async Task AddTransactionTestAsync()
        {
            var userManagerServiceMock = new Mock<IUserManagerService>();
            var loggerMock = new Mock<ILogger<PersonalFinanceController>>();

            PersonalFinanceController personalFinanceController = new PersonalFinanceController(userManagerServiceMock.Object, loggerMock.Object);

            var result = await personalFinanceController.AddTransaction(new AddTransactionRequest { Amount = 500, Description = "test", UserId = 1, СategoryName = "games" });

            string message = string.Empty;

            if (result is OkObjectResult okResult)
            {
                message = okResult.Value as string;
            }

            Assert.Equal("Транзакция успешно записана!", message);
        }

        [Fact]
        public async Task GetUserTransactions()
        {
            var userTransactions = new List<Transaction>
            {
                new Transaction("test", 500)
            };

            var userManagerServiceMock = new Mock<IUserManagerService>();
            userManagerServiceMock
                .Setup(s => s.GetUserTransactions(1))
                .Returns(userTransactions);

            var loggerMock = new Mock<ILogger<PersonalFinanceController>>();

            var personalFinanceController = new PersonalFinanceController(userManagerServiceMock.Object, loggerMock.Object);

            var userTransactionsResult = await personalFinanceController.GetUserTransactions(1);

            List<Transaction> transactions = new List<Transaction>();

            if (userTransactionsResult is OkObjectResult okResult)
            {
                transactions = (okResult.Value as IEnumerable<Transaction>)?.ToList();
            }

            Assert.Equal(userTransactions[0].Description, transactions[0].Description);
        }

        [Fact]
        public async Task GetAnalyzes()
        {
            var userManagerServiceMock = new Mock<IUserManagerService>();
            var loggerMock = new Mock<ILogger<PersonalFinanceController>>();

            List<Analysis> analysis =
            [
                new Analysis("games", 1000, 900)
            ];

            userManagerServiceMock
                .Setup(x => x.GetAnalyzes(1))
                .Returns(analysis);

            var personalFinanceController = new PersonalFinanceController(userManagerServiceMock.Object, loggerMock.Object);

            var AnalysisList = await personalFinanceController.GetAnalyzes(1);

            var result = new List<Analysis>();

            if (AnalysisList is OkObjectResult okResult)
            {
                result = (okResult.Value as IEnumerable<Analysis>)?.ToList();
            }

            List<Analysis> expectedResult =
            [
                new Analysis("games", 1000, 900)
            ];

            Assert.Equal(expectedResult, result);
        }
    }
}