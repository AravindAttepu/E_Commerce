using innobyte_e_commerce.Data;
using innobyte_e_commerce.DTO;
using innobyte_e_commerce.Models;
using innobyte_e_commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;

namespace innobyte_e_commerce.Controllers
{
    [Route("[controller]")]

    public class AdminController : Controller
    {
         private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _baseImageUrl;
         private readonly IJwtValidation _jwtValidation;

          public AdminController(AppDbContext context, IWebHostEnvironment environment,IJwtValidation jwtValidation)
        {
            _context = context;
            _environment = environment;
            _baseImageUrl = "https://localhost:5001/images/";
              _jwtValidation= jwtValidation;
        }
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        


        // GET: Admin
       
         [HttpGet("Upload")]
        public IActionResult Upload()
        {
             if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login","Admin");
            return View();
        }


        [HttpPost("uploadimage")]
public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
{

    if (file == null || file.Length == 0)
        return BadRequest(new { message = "No file uploaded" });
Console.WriteLine("got the images");
    // Ensure wwwroot exists (in case it's missing)
    var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    var imagesFolderPath = Path.Combine(wwwRootPath, "images");

    if (!Directory.Exists(imagesFolderPath))
    {
        Directory.CreateDirectory(imagesFolderPath);
    }

    // Generate a unique filename
    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
    var filePath = Path.Combine(imagesFolderPath, fileName);

    // Save the file
    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    // Generate the image URL
    var imageUrl = fileName;

    return Ok(new { imageUrl });
}


  [HttpPost("addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] uploadproduct product)
        {
             if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login","Admin");
            if (product == null)
                return BadRequest(new { message = "Invalid product data" });
                var seller= await _context.Sellers.FirstAsync(s => s.SellerId == product.SellerId);
            Product product1= new Product{
                ProductName= product.ProductName,
                Description= product.Description,
                Price= product.Price,
                category= product.category,
                SellerId= product.SellerId,
                Stock= product.Stock,
                ImageUrl= product.ImageUrl,
                seller =seller
            };
            await _context.products.AddAsync(product1);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product added successfully", product });
        }


    


         [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if ("admin@ecommerce.com" == userLogin.UserEmail && "admin@1234" == userLogin.Password)
        {
            HttpContext.Session.SetString("IsAdmin", "true"); // Store admin session
            return RedirectToAction("Index");
        }
        

        ViewBag.Error = "Invalid username or password!";
        return View();
        }
         [HttpGet("Login")]
        public IActionResult Login()
        {if (HttpContext.Session.GetString("IsAdmin") == "true")
            return RedirectToAction("Index");

            return View();
        }
          public IActionResult Index()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login","Admin");

        return View("Upload");
    }

    // ✅ Admin Logout
    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("IsAdmin"); // Destroy session
        return RedirectToAction("Login");
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListProducts()
    {
         if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login","Admin");
      return View();

    }
    [HttpGet("orders")]
    public async Task<IActionResult> AllOrders()
    {  // Fetch Master Orders first
     if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login","Admin");
var masterOrders = await _context.MasterOrders.ToListAsync();  
// var products1 = await _context.products
//     .Select( p=> p.ProductId)
//     .ToListAsync();

// Fetch Products for SellerID = 2
// var products = await _context.products
//     .Where(p => p.SellerId == 2)
//     .ToListAsync();

// Extract Product IDs
// var productIds = products.Select(p => p.ProductId).ToList(); 

// Fetch Orders where ProductID is in the extracted list
// var adminOrders = await _context.Orders.Include(o=>o.User)
//     .Where(o => products1.Contains(o.ProductID))
//     .ToListAsync();

    var adminorderids= masterOrders.Select(a=> a.MasterID).ToList();

// Fetch MasterOrders where MasterID is in the extracted product list
var filteredMasterOrders = _context.MasterOrders
    .Where(m => adminorderids.Contains(m.MasterID))
    .ToList();


var viewAdminOrdersList = new List<View_Admin_orders>();

foreach(var order in masterOrders)
{
    var totalorders= new View_Admin_orders {
        MasterOrder= order,
         Orders =await _context.Orders.Include(O =>O.Product).Include(o=>o.User).Where(a => a.MasterID == order.MasterID).ToListAsync() // Extract Orders
    
    };
    Console.WriteLine(JsonConvert.SerializeObject(totalorders, Formatting.Indented));
     viewAdminOrdersList.Add(totalorders);
}


    return View(viewAdminOrdersList);
    }


}
}

