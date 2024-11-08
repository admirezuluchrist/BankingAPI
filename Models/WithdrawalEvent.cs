

namespace BankingAPI.Models
{
    
    public class WithdrawalEvent
    {
        public decimal Amount { get; set; }
        public long AccountId { get; set; }
        public string Status { get; set; }

    }
}
