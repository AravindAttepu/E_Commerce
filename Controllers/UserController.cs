using E_commerce.Controllers;
using E_commerce.Models;
using innobyte_e_commerce.Data;
using innobyte_e_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace innobyte_e_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
         private readonly ILogger<UserController> _logger;
    private readonly AppDbContext _context;
    

    public UserController(ILogger<UserController> logger, AppDbContext context)
   { 
        _logger = logger;
        _context= context;
    }

         public async Task<IActionResult> Index()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
              if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login","Auth"); // Redirect to login if not authenticated
            }
            var user= await _context.Users.FirstAsync(u => u.Email == email);
        var cart= await _context.Carts.FirstAsync(c => c.UserID == user.UserID);
        var products= await _context.products.Select(p =>p).ToListAsync();
        //var cartitems = await _context.CartItems.Where( c => c.CartID == cart.CartID).ToListAsync();


          var cartitems = await _context.CartItems
        .Where(c => c.CartID == cart.CartID)
        .GroupBy(c => c.ProductID)
        .Select(g => new CartItemsView
        {
            ProductID = g.Key,
            Product = _context.products.FirstOrDefault(p => p.ProductId == g.Key),  // Fetch the Product Name
            Quantity = g.Sum(c => c.Quantity),
            Total = g.Sum(c => c.Total) // Sum of Total Price
        })
        .ToListAsync();

        var orderproducts=  await _context.Orders
        .Where(c => c.UserID == user.UserID)
        .GroupBy(c => c.ProductID)
        .Select(g => new CartItemsView
        {
            ProductID = g.Key,
            Product = _context.products.FirstOrDefault(p => p.ProductId == g.Key),  // Fetch the Product Name
            Quantity = g.Sum(c => c.Quantity),
            Total = g.Sum(c => c.TotalPrice) // Sum of Total Price
        })
        .ToListAsync();
       // var orders= await _context.Orders.Where( m=> m.UserID == user.UserID).ToListAsync();
        var userdata= new UserViewModel{
            UserDetails= user,
            CartProducts= cartitems,
            UserOrders= orderproducts,
            GrandTotal= cart.GrandTotal
        };
        
            return View(userdata);
          
        }
    }
}
