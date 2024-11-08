using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using BankingAPI.Managers.Interfaces;
using BankingAPI.Models;
using BankingAPI.Models.Responses;
using BankingAPI.Repositories.Interfaces;

namespace BankingAPI.Managers
{
    public class BankAccountManager : IBankAccountManager
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;
        private readonly IAmazonSimpleNotificationService _snsClient;

        public BankAccountManager(IConfiguration configuration, IAccountRepository accountRepository, IAmazonSimpleNotificationService snsClient)
        {
            _configuration = configuration;
            _accountRepository = accountRepository;
            _snsClient = snsClient;

        }
        public async Task<AccountResponse> Withdraw(long accountId, decimal amount)
        {
            try
            {
                var account = await _accountRepository.GetAccountAsync(accountId);
                if (account == null)
                {
                    return new AccountResponse { StatusCode = StatusCodes.Status404NotFound, Message = "Account not found" };
                }

                if (account.Balance < amount)
                {
                    return new AccountResponse { StatusCode = StatusCodes.Status400BadRequest, Message = "Insufficient funds for withdrawal" };
                    
                }
                await _accountRepository.WithdrawAsync(accountId, amount);
                var eevent = new WithdrawalEvent { Amount = amount, AccountId = accountId, Status = "SUCCESSFUL" };
                string eventJson = System.Text.Json.JsonSerializer.Serialize(eevent);
                string snsTopicArn = _configuration["SnsTopicArn"];

                PublishRequest publishRequest = new PublishRequest
                {
                    Message = eventJson,
                    TopicArn = snsTopicArn
                };

                PublishResponse publishResponse = await _snsClient.PublishAsync(publishRequest);
                return new AccountResponse { StatusCode = StatusCodes.Status200OK, Message = "Withdrawal successful" };
                

            }
            catch (Exception ex)
            {
                return new AccountResponse { StatusCode = StatusCodes.Status500InternalServerError, Message = "Withdrawal Unsuccessful" };
            }
        }
    }
}
