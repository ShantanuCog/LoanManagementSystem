// Defines the User model with necessary properties.
using System.ComponentModel.DataAnnotations;
using LoanManagementSystem.Models.Enums;

namespace LoanManagementSystem.Models;

public class User
{
    public Guid Id { get; set; }            // Unique identifier

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }       // Unique Email
    public string Username { get; set; }

    [Required]
    [StringLength(255)]
    public string Password { get; set; }    // Hashed Password

    [Required]
    public UserRoles Role { get; set; } // Admin, Borrower, Loan Officer - Many roles per user
    // Get Loan values
    public LoanStatus LoanStatus { get; set; }
}
