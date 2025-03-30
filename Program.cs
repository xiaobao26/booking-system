using System.Text;
using booking_system.Data;
using booking_system.Infrastructure.Authentication;
using booking_system.Mappings;
using booking_system.Repositories;
using booking_system.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

#region Connection Database
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration["DBConnection"]);
});
#endregion

#region AutoMapper dependency injection
builder.Services.AddAutoMapper(typeof(UserProfile));
#endregion

#region Service dependency injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
#endregion

#region Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWTConfigure:Issuer"],
            ValidAudience = builder.Configuration["JWTConfigure:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfigure:SecretKey"]))
        };
    });

builder.Services.AddAuthorization();
#endregion




builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

