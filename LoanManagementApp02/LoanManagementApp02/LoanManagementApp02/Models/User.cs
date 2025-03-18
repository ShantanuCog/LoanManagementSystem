// Defines the User model with necessary properties.
public class User
{
    public Guid Id { get; set; }            // Unique identifier
    public string Name { get; set; }
    public string Email { get; set; }       // Unique Email
    public string Password { get; set; }    // Hashed Password
	//public string Role { get; set; } // Admin, Borrower, Loan Officer - Many roles per user
}
