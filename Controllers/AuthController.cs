using System.Collections.Generic;
using BCrypt.Net;
using innobyte_e_commerce.Data;
using innobyte_e_commerce.DTO;
using innobyte_e_commerce.Models;
using innobyte_e_commerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Org.BouncyCastle.Crypto.Generators;

namespace innobyte_e_commerce.Controllers
{
    [Route("[controller]")]
  
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthController> _logger;
        private readonly IJwtValidation _jwtValidation;
        
        public AuthController(AppDbContext context, ILogger<AuthController> logger,IJwtValidation jwtValidation)
        {
            _context= context;
            _logger= logger;
            _jwtValidation= jwtValidation;
        }
[HttpGet("register")]
public async Task<IActionResult> Register()
{
    
    return View();
}
[HttpGet("login")]
public async Task<IActionResult> Login()
{
      if (Request.Cookies.ContainsKey("AuthToken"))
    {
         _logger.LogInformation("User already authenticated, redirecting to home.");
        return RedirectToAction("Index", "Home");
    }
    return View();
}




        [HttpPost("registeruser")]
        public async Task<IActionResult> RegisterUser( UserRegister userRegister)
        {
           
            Console.WriteLine(userRegister.Password);
            if(!ModelState.IsValid)
            {
               
                TempData["ErrorMessage"] = "Please fill all required fields correctly.";
                  return RedirectToAction("Register");
            }
            var ExistingUser= await _context.Users.FirstOrDefaultAsync(u => u.Email == userRegister.Email);
            if(ExistingUser!= null)
            {
                 TempData["ErrorMessage"] = "Email already in use.";
                  return RedirectToAction("Register");
            }
            
            string HashedPassword= BCrypt.Net.BCrypt.HashPassword(userRegister.Password);
           // userRegister.Password= HashedPassword;
            User user= new User{
                Name= $"{userRegister.FirstName} {userRegister.LastName}",
                Email= userRegister.Email,
                PasswordHash= HashedPassword,
                Mobile= userRegister.Mobile,
                EMailVerified= false,
                MobileVerified= false

            };
            await _context.Users.AddAsync(user);
             await  _context.SaveChangesAsync();
         
          UserVerify userVerify = new UserVerify{
            UserID= user.UserID,
            EMailVerifiedToken="false",
            MobileVerifiedToken="false"
          };
          await _context.UserVerification.AddAsync(userVerify);
        Cart cart= new Cart{
            UserID= user.UserID,
            GrandTotal=0.00
        };
        await _context.Carts.AddAsync(cart);
             await  _context.SaveChangesAsync();
        _logger.LogInformation("User registered successfully: {Email}", user.Email);

          
         TempData["SuccessMessage"] = "User registered successfully!";

        return RedirectToAction("Register");

        }

        [HttpPost("login")]
        public async Task<IActionResult> loginuser(UserLogin userLogin)
        {
            if (Request.Cookies.ContainsKey("AuthToken"))
    {
         _logger.LogInformation("User already authenticated, redirecting to home.");
        return RedirectToAction("Index", "Home");
    }
           
            if(!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Enter the  required details";
                return RedirectToAction("login");
            }
            var UserExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLogin.UserEmail);
            if(UserExists== null)
            {
              TempData["ErrorMessage"] = "Enter the details correctly";
                return RedirectToAction("login");
            }
            bool isValidPassword= BCrypt.Net.BCrypt.Verify(userLogin.Password, UserExists.PasswordHash);

            if(!isValidPassword)
            {
                
                TempData["ErrorMessage"] = "Invalid Username or Password";
                return RedirectToAction("login");
            }
            var JWT= _jwtValidation.GenerateToken(UserExists.Email,"User");
            _logger.LogInformation($"jwt Generated for user{UserExists.Email}");
            

           Response.Cookies.Append("AuthToken", JWT, new CookieOptions
    {
        HttpOnly = true,  // Prevents JavaScript access (security best practice)
        Secure = true,    // Ensures it's sent only over HTTPS
        SameSite = SameSiteMode.Strict, // Prevents CSRF attacks
           Expires = DateTime.UtcNow.AddDays(7)
    });

    _logger.LogInformation($"JWT generated for user {UserExists.Email}");

    return RedirectToAction("Index","Home"); // Redirect to another page after lo
        }


         [HttpGet("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        return RedirectToAction("Login");
    }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            List<User> list= await _context.Users.Select(u=>u).ToListAsync();
            return Ok(list);
        }

    //     public async Task<bool> InsertEmailToken(string token,int UserID)
    //     {
    //         var UserExists= await _context.Users.FirstOrDefaultAsync(u => u.UserID == UserID);
    //         if(UserExists == null)
    //         {
    //             return false;
    //         }
    //         string token = Guid.NewGuid().ToString();
    //    InsertEmailToken(token,user.UserID);

    //     string verificationLink = $"{Request.Scheme}://{Request.Host}/api/users/verify?email={model.Email}&token={token}";
    //     await _emailService.SendVerificationEmail(model.Email, verificationLink);
            
    //         _context.UserVerification.Add(token);


    //     }
    }
}
