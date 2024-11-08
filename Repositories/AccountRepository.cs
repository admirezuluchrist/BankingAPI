using BankingAPI.Models;
using BankingAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace BankingAPI.Repositories
{
  

    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _configuration;

        public AccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Account> GetAccountAsync(long accountId)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                string sql = "SELECT balance FROM accounts WHERE id = @accountId";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@accountId", accountId);

                decimal balance = (decimal)await command.ExecuteScalarAsync();
                return new Account { Id = accountId, Balance = balance };
            }
        }

        public async Task WithdrawAsync(long accountId, decimal amount)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                string sql = "UPDATE accounts SET balance = balance - @amount WHERE id = @accountId";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@accountId", accountId);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 0)
                {
                    throw new Exception("No rows affected");
                }
            }
        }
        
    }
}
