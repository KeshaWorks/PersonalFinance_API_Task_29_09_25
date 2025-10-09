using Moq;
using Project.Interfaces;
using Microsoft.Extensions.Logging;
using Project.Services;
using Project.Models;

namespace PersonalFinanceTest.ServicesTests
{
    public  class RecommendationManagerServiceTests
    {
        [Fact]
        public void GetInsightsSavings()
        {
            var repositorieMock = new Mock<IUserManagerRepositorie>();
            var loggerMock = new Mock<ILogger<RecommendationManagerService>>();

            List<Category> categories =
            [
                new Category{ CategoryName = "Test", Limit = 1000, Transactions = [new Transaction("Test", 900)]}
            ];

            User user = new User { UserId = 1, Categories = categories };

            repositorieMock.Setup(x => x.GetUserById(1))
                    .Returns(user);

            var recommendationManagerService = new RecommendationManagerService(repositorieMock.Object, loggerMock.Object);

            InsightSaving[] expectedResult = new InsightSaving[3];

            decimal limit = 1000;
            decimal totalSum = 900;
            expectedResult[0] = new InsightSaving($"«Вы использовали {Math.Round((totalSum / limit) * 100)}% бюджета в категории \"{{{"Test"}}}\". " +
                $"Рассмотрите возможность сократить расходы.»", totalSum / 2, "Test");

            InsightSaving[] result = recommendationManagerService.GetInsightsSavings(1);

            Assert.Equal(expectedResult, result);
        }
    }
}