using BankingAPI.Models;
using System.Data;
using System.Threading.Tasks;
namespace BankingAPI.Repositories.Interfaces
{


    public interface IAccountRepository
    {
        Task<Account> GetAccountAsync(long accountId);
        Task WithdrawAsync(long accountId, decimal amount);

    }
}
