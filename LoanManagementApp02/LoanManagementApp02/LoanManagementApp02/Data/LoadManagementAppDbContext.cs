public class LoadManagementAppDbContext : DbContext
{
	public LoadManagementAppDbContext(DbContextOptions<LoadManagementAppDbContext> options) : base(options) { }
	public DbSet<User> Users { get; set; }		// Users Table
}
