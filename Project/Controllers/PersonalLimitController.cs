using Microsoft.AspNetCore.Mvc;
using Project.Interfaces;
using Project.Models.TakedFromBody;

namespace Project.Controllers
{
    /// <summary>
    /// Controller for set and managment limit
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalLimitController : ControllerBase
    {
        private ILimitManagerService _limitManagerService;

        private readonly ILogger<PersonalLimitController> _logger;

        public PersonalLimitController 
            (
            ILimitManagerService userManagerRepositorie, 
            ILogger<PersonalLimitController> logger
            )
        {
            _limitManagerService = userManagerRepositorie;
            _logger = logger;
        }

        /// <summary>
        /// Add limit for specified categorie
        /// </summary>
        /// <param name="addLimitRequest"></param>
        /// <returns></returns>
        [HttpPut("budgets")]
        public async Task<IActionResult> AddLimit(AddLimitRequest addLimitRequest)
        {
            _logger.LogInformation($"Установка лимита для {addLimitRequest.UserId}");
            _limitManagerService.AddLimit(addLimitRequest);
            return Ok("Лимит успешно установлен!");
        }
    }
}