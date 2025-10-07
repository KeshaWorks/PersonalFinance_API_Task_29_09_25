using Microsoft.AspNetCore.Mvc;
using Project.Interfaces;
using Project.Models.TakedFromBody;
using System.ComponentModel.DataAnnotations;

namespace Project.Controllers
{
    /// <summary>
    /// Controller for managment person's finance
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalFinanceController : ControllerBase
    {
        private IUserManagerService _userManagerService;

        private readonly ILogger<PersonalFinanceController> _logger;

        public PersonalFinanceController(IUserManagerService userManagerService, ILogger<PersonalFinanceController> logger)
        {
            _userManagerService = userManagerService;
            _logger = logger;
        }

        /// <summary>
        /// Add Transaction for specified user
        /// </summary>
        /// <param name="addTransactionRequest"></param>
        /// <returns></returns>
        [HttpPost("transactions")]
        public async Task<IActionResult> AddTransaction(AddTransactionRequest addTransactionRequest)
        {
            _logger.LogInformation("Записывание транзакции");
            _userManagerService.AddTransaction(addTransactionRequest);
            return Ok("Транзакция успешно записана!");
        }

        /// <summary>
        /// Get User's Transactions in every categorie
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("transactions")]
        public async Task<IActionResult> GetUserTransactions([FromQuery][Range(1, int.MaxValue)] int userId)
        {
            _logger.LogInformation("Получение Транзакций пользователя");
            return Ok(_userManagerService.GetUserTransactions(userId));
        }

        /// <summary>
        /// Get information about every categorie
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("budgets/analysis")]
        public async Task<IActionResult> GetAnalyzes([FromQuery][Range(1, int.MaxValue)] int userId)
        {
            _logger.LogInformation("Получения анализа о категории");
            return Ok(_userManagerService.GetAnalyzes(userId));
        }
    }
}