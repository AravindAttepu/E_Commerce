using System;
using System.Security.Claims;

namespace innobyte_e_commerce.Services;

public interface IJwtValidation
{
     public string GenerateToken(string Name,string Role);
      public ClaimsPrincipal ValidateToken(string token);

}
