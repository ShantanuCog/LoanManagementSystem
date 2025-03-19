using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using LoadManagementApp.Data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

class Program
{
    // create property of builder of type WebApplicationBuilder
    public static WebApplicationBuilder builder { get; set; }
    public static byte[] key { get; set; }

    public static void Main(string[] args)
    {
        // instantiate/create builder object
        builder = WebApplication.CreateBuilder(args);

        // Configure JWT Authentication. Using information from 'AppSetting.json'
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        Console.WriteLine(jwtSettings["Key"]);

        // instantiate.create key object
        key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

        // Add services. "importing" the controller details from 'AuthController.cs'
        builder.Services.AddControllers();
        // Add DbContext (Database Connection) to DI
        builder.Services.AddDbContext<LoadManagementAppDbContext>(options =>
            options.UseSqlite(    // change to 'UseSqlServer'
                builder.Configuration.GetConnectionString("DefaultConnection")));    // Refers to appsettings.json

        // Adds the authentication to the web server (using builder instance)
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })

        // creates the context for where the token is coming from (how the token is dealt with). Server creates the token after login
        // JWT stored in DB + sent to user. Compare the token in DB with user whenever it is requested DURING any logged in session. Username and password compared AT login
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        // Build app
        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

