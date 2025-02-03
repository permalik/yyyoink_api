using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace YYYoinkAPI.Services.AuthN;

public class JwtGenerator
{
    public string GenerateJwt(Guid uuid, string email)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        string? keyStr = Environment.GetEnvironmentVariable("JWT_KEY") ?? string.Empty;
        if (string.IsNullOrWhiteSpace(keyStr))
        {
            throw new InvalidOperationException("JWT_KEY is invalid or missing during JWT Generation");
        }

        byte[] key = Encoding.UTF8.GetBytes(keyStr);

        List<Claim> claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, uuid.ToString()),
            new(JwtRegisteredClaimNames.Email, email)
        };

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
            Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}