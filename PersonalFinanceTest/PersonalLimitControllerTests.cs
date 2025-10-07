using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Project.Controllers;
using Project.Interfaces;
using Project.Models.TakedFromBody;

namespace PersonalFinanceTest
{
    public class PersonalLimitControllerTests
    {
        [Fact]
        public async Task AddLimit()
        {
            var limitManagerService = new Mock<ILimitManagerService>();
            var loggerMock = new Mock<ILogger<PersonalLimitController>>();

            var personalLimitController = new PersonalLimitController(limitManagerService.Object, loggerMock.Object);

            var result = await personalLimitController.AddLimit(new AddLimitRequest { Limit = 1000, UserId = 1, СategoryName = "games" });

            string message = string.Empty;

            if (result is OkObjectResult okResult)
            {
                message = okResult.Value as string;
            }

            Assert.Equal("Лимит успешно установлен!", message);
        }
    }
}