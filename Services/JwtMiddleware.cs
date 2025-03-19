using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using innobyte_e_commerce.Services;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly IJwtValidation _jwtvalidation;

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration, IJwtValidation jwtValidation)
    {
        _next = next;
        _configuration = configuration;
        _jwtvalidation= jwtValidation;
    }


    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Cookies["AuthToken"];

        if (!string.IsNullOrEmpty(token))
        {
            var principal = _jwtvalidation.ValidateToken(token);
            if (principal != null)
            {
                context.User = principal;
            }
            else
        {
            // Token is invalid or expired, delete the cookie and redirect
            context.Response.Cookies.Delete("AuthToken");
            context.Response.Redirect("/Auth/Login");
            return;
        }
        }

        await _next(context);
    }
}
