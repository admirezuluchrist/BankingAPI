using Microsoft.AspNetCore.Mvc;
using BankingAPI.Managers.Interfaces;
using BankingAPI.Models.Responses;

namespace BankingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {

        private readonly IBankAccountManager _bankAccountManager;
        public BankAccountController(IBankAccountManager bankAccountManager)
        {
            _bankAccountManager = bankAccountManager;
        }

        [HttpPost("withdraw")]
        public async Task<AccountResponse> Withdraw(long accountId, decimal amount)
        {
            return await _bankAccountManager.Withdraw(accountId, amount);
        }
    }
}
