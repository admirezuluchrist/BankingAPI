using BankingAPI.Models.Responses;

namespace BankingAPI.Managers.Interfaces
{
    public interface IBankAccountManager
    {
        Task<AccountResponse> Withdraw(long accountId, decimal amount);
    }
}
