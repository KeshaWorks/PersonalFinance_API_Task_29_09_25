using Microsoft.Extensions.Logging;
using Moq;
using Project.Interfaces;
using Project.Models;
using Project.Services;

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

        [Fact]
        public void Get_insights_savings_when_categories_empty()
        {
            var repositorieMock = new Mock<IUserManagerRepositorie>();
            var loggerMock = new Mock<ILogger<RecommendationManagerService>>();

            User user = new User { UserId = 1, Categories = new List<Category>() };

            repositorieMock.Setup(x => x.GetUserById(1))
                    .Returns(user);

            var recommendationManagerService = new RecommendationManagerService(repositorieMock.Object, loggerMock.Object);

            InsightSaving[] expectedResult = new InsightSaving[3];

            InsightSaving[] result = recommendationManagerService.GetInsightsSavings(1);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Get_insights_savings_when_categories_more_3()
        {
            var repositorieMock = new Mock<IUserManagerRepositorie>();
            var loggerMock = new Mock<ILogger<RecommendationManagerService>>();

            List<Category> categories =
            [
                new Category{ CategoryName = "Test1", Limit = 10, Transactions = [new Transaction("Test1", 9)]},
                new Category{ CategoryName = "Test2", Limit = 100, Transactions = [new Transaction("Test2", 90)]},
                new Category{ CategoryName = "Test3", Limit = 1000, Transactions = [new Transaction("Test3", 900)]},
                new Category{ CategoryName = "Test4", Limit = 1000, Transactions = [new Transaction("Test4", 500)]},
                new Category{ CategoryName = "Test5", Limit = 10000, Transactions = [new Transaction("Test5", 9000)]}
            ];

            User user = new User { UserId = 1, Categories = categories };

            repositorieMock.Setup(x => x.GetUserById(1))
                    .Returns(user);

            var recommendationManagerService = new RecommendationManagerService(repositorieMock.Object, loggerMock.Object);

            InsightSaving[] expectedResult =
            {
                new InsightSaving
                (
                    $"«Вы использовали 90% бюджета в категории Test2. " +
                    $"Рассмотрите возможность сократить расходы.»", 
                    45,
                    "Test2"
                ),
                new InsightSaving
                (
                    $"«Вы использовали 90% бюджета в категории Test3. " +
                    $"Рассмотрите возможность сократить расходы.»",
                    450,
                    "Test3"
                ),
                new InsightSaving
                (
                    $"«Вы использовали 90% бюджета в категории Test5. " +
                    $"Рассмотрите возможность сократить расходы.»",
                    4500,
                    "Test5"
                ),
            };

            InsightSaving[] result = recommendationManagerService.GetInsightsSavings(1);

            Assert.Equal(expectedResult.Length, result.Length);
        }
    }
}