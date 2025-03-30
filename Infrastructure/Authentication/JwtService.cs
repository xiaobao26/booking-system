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
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ]),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWTConfigure:ExpirationInMinutes")),
            SigningCredentials = creds,
            Issuer = _configuration["JWTConfigure:Issuer"],
            Audience = _configuration["JWTConfigure:Audience"]
        };

        var handler = new JsonWebTokenHandler();
        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}