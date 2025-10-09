using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Project.Controllers;
using Project.Interfaces;
using Project.Models;

namespace PersonalFinanceTest.ControllersTests
{
    public class PersonalRecommendationControllerTests
    {
        [Fact]
        public async Task GetInsightsSavings()
        {
            var recommendationManagerService = new Mock<IRecommendationManagerService>();
            var loggerMock = new Mock<ILogger<PersonalRecommendationController>>();

            InsightSaving[] insightSavings =
            [
                new InsightSaving("Test", 2345, "games")
            ];

            recommendationManagerService
                .Setup(x => x.GetInsightsSavings(1))
                .Returns(insightSavings);

            var personalRecommendationController = new PersonalRecommendationController(recommendationManagerService.Object, loggerMock.Object);

            var insightsList = await personalRecommendationController.GetInsightsSavings(1);

            var result = new InsightSaving[3];

            if (insightsList is OkObjectResult okResult)
            {
                result = (okResult.Value as IEnumerable<InsightSaving>)?.ToArray();
            }

            InsightSaving[] expectedResult = 
            [
                new InsightSaving("Test", 2345, "games")
            ];

            Assert.Equal(expectedResult[0], result[0]);
        }
    }
}