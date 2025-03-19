using Fluent.Infrastructure.FluentModel;
using LoanManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystem.Controllers
{
    [ApiController]
    [Route("api/loans")]
    public class LoanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LoanController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> Apply([FromBody] Loan loan)
        {
            try
            {
                loan.Id = Guid.NewGuid();
                loan.CreatedAt = DateTime.UtcNow;
                _context.Loan.Add(loan);
                await _context.SaveChangesAsync();

                return Ok("Loan application submitted successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoan(Guid id)
        {
            try
            {
                var loan = await _context.Loans.Include(l => l.User).SingleOrDefaultAsync(l => l.Id == id);
                if (loan == null)
                {
                    return NotFound("Loan not found.");
                }

                return Ok(loan);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveLoan(Guid id)
        {
            try
            {
                var loan = await _context.Loans.SingleOrDefaultAsync(l => l.Id == id);
                if (loan == null)
                {
                    return NotFound("Loan not found.");
                }

                loan.Status = "Approved";
                await _context.SaveChangesAsync();

                return Ok("Loan approved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectLoan(Guid id)
        {
            try
            {
                var loan = await _context.Loans.SingleOrDefaultAsync(l => l.Id == id);
                if (loan == null)
                {
                    return NotFound("Loan not found.");
                }

                loan.Status = "Rejected";
                await _context.SaveChangesAsync();

                return Ok("Loan rejected successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
