using Microsoft.EntityFrameworkCore;
using LoanManagementSystem.Models;
using LoanManagementSystem.Models.Enums;

/*-------------Responsibility-------------
Managing the database connection and configuring the database context.
Defining DbSet properties for entities: User, Feedback, Rota, Tasks, to enable CRUD operations on the corresponding database tables. */

namespace LoadManagementSystem.Data;

public class LoadManagementAppDbContext : DbContext
{
    public LoadManagementAppDbContext(DbContextOptions<LoadManagementAppDbContext> options) : base(options) { }
	public DbSet<User> Users { get; set; }		// Users Table
    public DbSet<Loan> Loans { get; set; }
}
