using Microsoft.AspNetCore.Mvc;
using Project.Interfaces;
using Project.Models;

namespace Project.Controllers
{
    //Сделать общий анализ
    //Добавить тесты
    //Добавить валидация атрибутам или fluent
    //Добавить логирование(для каждого сервиса), для контроллер вслучае если будут ошибки

    /// <summary>
    /// Controller for managment person's finance
    /// </summary>
    [Route("api")]
    [ApiController]
    public class PersonalFinanceController : ControllerBase
    {
        private IUserManagerService _userManagerService;

        public PersonalFinanceController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        /// <summary>
        /// Add Transaction for specified user
        /// </summary>
        /// <param name="addTransactionRequest"></param>
        /// <returns></returns>
        [HttpPost("transactions")]
        public async Task<IActionResult> AddTransaction(AddTransactionRequest addTransactionRequest)
        {
            _userManagerService.AddTransaction(addTransactionRequest);
            return Ok("Транзакция успешно записана!");
        }

        /// <summary>
        /// Get User's Transactions in every categorie
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("transactions")]
        public async Task<IActionResult> GetUserTransactions([FromQuery] int userId)
        {
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
            _userManagerService.AddLimit(addLimitRequest);
            return Ok("Лимит успешно установлен!");
        }

        /// <summary>
        /// Get information about every categorie
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("budgets/analysis")]
        public async Task<IActionResult> GetAnalyzes([FromQuery] int userId)
        {
            return Ok(_userManagerService.GetAnalyzes(userId));
        }

        /// <summary>
        /// Get 3 recommendation about expenses
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("insights/savings")]
        public async Task<IActionResult> GetInsightsSavings([FromQuery] int userId)
        {
            return Ok(_userManagerService.GetInsightsSavings(userId));
        }
    }
}