using Newtonsoft.Json;

using System.Text.Json.Serialization;
using System.Transactions;
using E_commerce.Models;
using innobyte_e_commerce.Data;
using innobyte_e_commerce.DTO;
using innobyte_e_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace innobyte_e_commerce.Controllers
{
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly AppDbContext _context;

        public OrdersController(ILogger<OrdersController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("setorder")]
        public async Task<IActionResult> SetOrder([FromBody] Address address)
        {
            //using var transaction = await _context.Database.BeginTransactionAsync();
            var email = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserID == user.UserID);
            if (cart == null)
            {
                return BadRequest("Cart not found.");
            }

            var cartItems = await _context.CartItems.Where(c => c.CartID == cart.CartID).ToListAsync();
            int itemCount = await _context.CartItems
                             .Where(c => c.CartID == cart.CartID)
                             .CountAsync();
            if (!cartItems.Any())
            {
                return BadRequest("Cart is empty.");
            }

           
            // int lastMasterId = _context.MasterOrders.OrderByDescending(m => m.MasterID).Select(m => m.MasterID).FirstOrDefault();
            // int newMasterId = lastMasterId + 1;
            // foreach(var product in cartItems)
            // {
            // if(product.Product.Stock < product.Quantity)
            // {
            //      await transaction.RollbackAsync();
            //   return Ok(new { message = "Insufficient Stock", product.Product.ProductName  });  
            // }
          
             
            // }
            double grandTotal = cartItems.Sum(item => item.Total);
            string final_address= JsonConvert.SerializeObject(address);
          
            var masterOrder = new MasterOrder
            {
                UserID = user.UserID,
                GrandTotal = grandTotal,
                Address = final_address,
                TotalItems=itemCount,
             Date = DateOnly.FromDateTime(DateTime.UtcNow),
             PaymentMethod="COD"

            };

            await _context.MasterOrders.AddAsync(masterOrder);
            await _context.SaveChangesAsync(); 
            _logger.LogInformation("master id created");

          
            int generatedMasterId = masterOrder.MasterID;

            // Insert Orders
            foreach (var item in cartItems)
            {
                    var order = new Order
                    {
                        ProductID = item.ProductID,
                        UserID = user.UserID,
                        Price = item.Total,
                        Quantity = item.Quantity,
                        TotalPrice = item.Total,
                        MasterID = generatedMasterId 
                    };
          

               await   _context.Orders.AddAsync(order);
                _logger.LogInformation("order are added");
                 _logger.LogInformation("Order Details: {OrderJson}", JsonConvert.SerializeObject(order, Formatting.Indented));
                 
            }
_logger.LogInformation("orders are created");
            await _context.SaveChangesAsync(); 
 
             _context.CartItems.RemoveRange(cartItems);
              cart.GrandTotal = 0;
    _context.Carts.Update(cart);

    await _context.SaveChangesAsync(); 
 _logger.LogInformation("cart has become  empty");

            return Ok(new { message = "Order placed successfully", masterOrder, orders = cartItems });
        }
        [HttpGet("place_order")]
        public async Task<IActionResult> PlaceOrder()
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


         

       
       // var orders= await _context.Orders.Where( m=> m.UserID == user.UserID).ToListAsync();
     
            return View(cart);
        }
    }
}
