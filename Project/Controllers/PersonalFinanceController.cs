using Microsoft.AspNetCore.Mvc;
using Project.Interfaces;
using Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Project.Controllers
{
    //1 Добавить тесты

    /// <summary>
    /// Controller for managment person's finance
    /// </summary>
    [Route("api")]
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
        /// Add limit for specified categorie
        /// </summary>
        /// <param name="addLimitRequest"></param>
        /// <returns></returns>
        [HttpPut("budgets")]
        public async Task<IActionResult> AddLimit(AddLimitRequest addLimitRequest) 
        {
            _logger.LogInformation("Установка лимита");
            _userManagerService.AddLimit(addLimitRequest);
            return Ok("Лимит успешно установлен!");
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

        /// <summary>
        /// Get 3 recommendation about expenses
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("insights/savings")]
        public async Task<IActionResult> GetInsightsSavings([FromQuery][Range(1, int.MaxValue)] int userId)
        {
            _logger.LogInformation("Получение советов о финансов");
            return Ok(_userManagerService.GetInsightsSavings(userId));
        }
    }
}