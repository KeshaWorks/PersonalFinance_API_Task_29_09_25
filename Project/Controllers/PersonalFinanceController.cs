using Microsoft.AspNetCore.Mvc;
using Project.Interfaces;
using Project.Models;

namespace Project.Controllers
{
    //Добавить коментарии
    //Сделать общий анализ
    //Добавить тесты

    [Route("api")]
    [ApiController]
    public class PersonalFinanceController : ControllerBase
    {
        private IUserManagerService _userManagerService;

        public PersonalFinanceController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        [HttpPost("transactions")]
        public async Task<IActionResult> AddTransaction(AddTransactionRequest addTransactionRequest)
        {
            _userManagerService.AddTransaction(addTransactionRequest);
            return NoContent();
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetUserTransactions([FromQuery] int userId)
        {
            return Ok(_userManagerService.GetUserTransactions(userId));
        }

        [HttpPut("budgets")]
        public async Task<IActionResult> AddLimit(AddLimitRequest addLimitRequest) 
        {
            _userManagerService.AddLimit(addLimitRequest);
            return NoContent();
        }

        [HttpGet("budgets/analysis")]
        public async Task<IActionResult> GetAnalyzes([FromQuery] int userId)
        {
            return Ok(_userManagerService.GetAnalyzes(userId));
        }

        [HttpGet("insights/savings")]
        public async Task<IActionResult> GetInsightsSavings([FromQuery] int userId)
        {

        }
    }
}