using Microsoft.AspNetCore.Mvc;
using Project.Interfaces;
using Project.Models;

namespace Project.Controllers
{
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

            return NoContent();
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetUserTransaction([FromQuery] int userId)
        {

        }

        [HttpPut("budgets")]
        public async Task<IActionResult> AddLimit(AddLimitRequest addLimitRequest) 
        {

            return NoContent();
        }

        [HttpGet("budgets/analysis")]
        public async Task<IActionResult> GetAnalyzes([FromQuery] int userId)
        {

        }

        [HttpGet("insights/savings")]
        public async Task<IActionResult> GetInsightsSavings([FromQuery] int userId)
        {

        }
    }
}