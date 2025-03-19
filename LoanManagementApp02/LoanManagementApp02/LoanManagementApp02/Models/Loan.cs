using LoanManagementSystem.Models;
using LoanManagementSystem.Models.Enums;

namespace LoanManagementSystem.Models
{
    public class Loan
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public decimal Amount { get; set; }
        public LoanStatus Status { get; set; } // Pending, Approved, Rejected, Disbursed
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
