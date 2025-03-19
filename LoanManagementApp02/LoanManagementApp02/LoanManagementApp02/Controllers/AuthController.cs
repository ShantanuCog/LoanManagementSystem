using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DevOne.Security.Cryptography.BCrypt;

using LoanManagementSystem.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using LoanManagementSystem.Models.Enums;
using Fluent.Infrastructure.FluentModel;

// Handles User Registration and Login
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

	public AuthController(ApplicationDbContext context, IConfiguration configuration)
	{
        _context = context;
        _configuration = configuration;
	}

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterModel user)
    {
        try
        {
            // Normally, save the user to a DB
            if (user.Username == "newuser" && user.Password == "newpassword123")
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Username = user.Name,
                    Email = user.Email,
                    Password = user.Password, // password
                    Role = user.Role
                };

                // Simulate adding user to the database
                // _context.Users.Add(newUser);
                // _context.SaveChanges();

                return Ok("User registered successfully.");
            }

            return BadRequest("User registration failed.");
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here if needed
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost("login")]    // method: Login. type: IActionResult. attribute: login. Form of dependency injection
    public IActionResult Login([FromBody] LoginModel user)
    {
        try
        {
            // Normally, validate the user credentials from a DB
            if (user.Username == "admin" && user.Password == "password123")
            // var storedUser = _context.Users.SingleOrDefault(u => u.Username == user.Username);
            // if (storedUser != null && BCrypt.Net.BCrypt.Verify(user.Password, storedUser.Password))
            {
                var token = GenerateJwtToken(user.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials.");
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here if needed
            return StatusCode(500, "Internal server error.");
        }
    }

    private string GenerateJwtToken(string username)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public UsersController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult GetUsers()
    {
        // Logic to list all users
        return Ok();
    }
}


    // Classes for Register and Login
    public class RegisterModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public UserRoles Role { get; set; }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}