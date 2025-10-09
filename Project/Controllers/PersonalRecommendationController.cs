using Microsoft.AspNetCore.Mvc;
using Project.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Project.Controllers
{
    /// <summary>
    /// Controller for getting recommendations about finance
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalRecommendationController : ControllerBase
    {
        private IRecommendationManagerService _recommendManagerService;

        private readonly ILogger<PersonalRecommendationController> _logger;

        public PersonalRecommendationController
            (
            IRecommendationManagerService recommendationManagerService, 
            ILogger<PersonalRecommendationController> logger
            )
        {
            _recommendManagerService = recommendationManagerService;
            _logger = logger;
        }

        /// <summary>
        /// Get 3 recommendation about expenses
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("insights/savings")]
        public async Task<IActionResult> GetInsightsSavings([FromQuery][Range(1, int.MaxValue)] int userId)
        {
            _logger.LogInformation($"Получение советов о финансов для {userId}");
            return Ok(_recommendManagerService.GetInsightsSavings(userId));
        }
    }
}