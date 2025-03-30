using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using booking_system.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;


namespace booking_system.Infrastructure.Authentication;

public class JwtService: IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(User user)
    {
        var secretKey = _configuration["JWTConfigure:SecretKey"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("email_verified", user.EmailVerified.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var claimIdentity = new ClaimsIdentity(claims);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimIdentity,
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWTConfigure:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = _configuration["JWTConfigure:Issuer"],
            Audience = _configuration["JWTConfigure:Audience"]
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        
        return handler.WriteToken(token);
    }
}