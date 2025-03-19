using innobyte_e_commerce.Data;
using innobyte_e_commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace innobyte_e_commerce.Controllers
{  
     [Authorize]
      [ApiController]
    [Route("[controller]")]

    public class CartController : Controller
    {

         private readonly ILogger<CartController> _logger;
    private readonly AppDbContext _context;
    

    public CartController(ILogger<CartController> logger, AppDbContext context)
    {
        _logger = logger;
        _context= context;
    }
    [HttpPost("addproduct")]
    public async Task<IActionResult> AddCartProduct([FromBody]Product product)
    {
      var email = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
              if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login","Auth"); // Redirect to login if not authenticated
            }
            var user= await _context.Users.FirstAsync(u => u.Email == email);
            var cart= await _context.Carts.FirstAsync( c=>c.UserID == user.UserID);
            CartItems cartItems = new CartItems{
                CartID= cart.CartID,
                ProductID= product.ProductId,
                Quantity= 1,
                Total= product.Price
            };
            Cart cartupdate = new Cart{
                UserID= user.UserID,
                GrandTotal= product.Price+cart.GrandTotal
            };

            await _context.CartItems.AddAsync(cartItems);
           
             await _context.Carts
                .Where(c => c.CartID == cart.CartID)  // Find the specific cart item
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.GrandTotal, cart.GrandTotal+product.Price)
                    
                );

                await _context.SaveChangesAsync();
                _logger.LogInformation("item added to cart");
        return Ok(new { message = "Item Added successfully", cartItems });
    }

    [HttpPost("DeleteCartQuantity")]
    public async Task<IActionResult> DeleteCartQuantity([FromBody] CartItemModify request)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserID == request.UserId);
          var product = await _context.products.FirstOrDefaultAsync( p=> p.ProductId == request.ProductId);
    if (cart == null)
    {
        return NotFound("Cart not found.");
    }

 var cartItem = await _context.CartItems
    .Where(c => c.CartID == cart.CartID && c.ProductID == request.ProductId)
    .FirstOrDefaultAsync();

if (cartItem != null)
{
    _context.CartItems.Remove(cartItem);
    await _context.SaveChangesAsync();

    //   Cart cartupdate = new Cart{
    //             UserID= request.UserId,
    //             GrandTotal= cart.GrandTotal-product.Price
    //         };
             await _context.Carts
                .Where(c => c.CartID == cart.CartID)  // Find the specific cart item
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.GrandTotal, cart.GrandTotal-product.Price)
                    
                );

                await _context.SaveChangesAsync();
}


    return Ok(new { message = "Item deleted successfully", cartItem });
    }

[HttpPost("AddCartQuantity")]
    public async Task<IActionResult> AddCartQuantity([FromBody] CartItemModify request)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserID == request.UserId);
        var product = await _context.products.FirstOrDefaultAsync( p=> p.ProductId == request.ProductId);
    if (cart == null || product == null)
    {
        return NotFound("Cart/Product  not found.");
    }
// CartItems cartItems= new CartItems{
//     CartID= cart.CartID,
//     ProductID= request.ProductId,
//     Quantity= 1,
//     Total=request.Price

// };
 return await AddCartProduct(product);




    // _context.CartItems.Add(cartItems);
    // await _context.SaveChangesAsync();



    // return Ok(new { message = "Item added successfully", cartItems });
    }



    }
}
