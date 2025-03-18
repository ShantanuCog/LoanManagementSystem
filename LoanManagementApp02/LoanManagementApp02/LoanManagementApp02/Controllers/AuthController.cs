// Handles User Registration and Login
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
	private readonly LMSDbContext _context;

	public AuthController(LMSDbContext context)
	{
		_context = context;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDto model)
	{
		var user = new User
		{
			Id = Guid.NewGuid(),
			Name = model.Name,
			Email = model.Email,
			Password = BCrypt.Net.BCrypt.HashPassword(model.Password), // Hash password
			Role = model.Role
		};

		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return Ok("User registered successfully.");
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginDto model)
	{
		var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
		if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
		{
			return Unauthorized("Invalid credentials.");
		}

		var token = GenerateJwtToken(user);
		return Ok(new { token });
	}

	private string GenerateJwtToken(User user)
	{
		// JWT Token generation logic
	}
}
