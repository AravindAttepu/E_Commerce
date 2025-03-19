using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace innobyte_e_commerce.Services;

public class JwtValidation :IJwtValidation
{

  private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiryMinutes;

      public JwtValidation(IConfiguration config)
    {
        var jwtSettings = config.GetSection("JwtSettings");
        _secretKey = jwtSettings["SecretKey"];
        _issuer = jwtSettings["Issuer"];

    }

    public string GenerateToken(string Name,string Role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds= new SigningCredentials(key,  SecurityAlgorithms.HmacSha256);

        List<Claim> claims= new List<Claim>{
           new Claim("name", Name),
            new Claim("role", Role)
        };
        var token= new JwtSecurityToken(
            issuer:_issuer,
            audience:null,
            claims:claims,
            expires:DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
            
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);

        try
        {
         // Console.Write("validating token");
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = false,
                ValidateLifetime = true,
                 ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch
        {
            return null;
        }
    }
}
