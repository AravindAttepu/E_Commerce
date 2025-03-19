using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Models;
using System.Security.Claims;
using innobyte_e_commerce.Data;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context= context;
    }

    public IActionResult Index()
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        return View("Index",email);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [HttpGet("fetchproducts")]
    public async Task<IActionResult> fetchproducts()
    {
        var products = await _context.products
    .Include(p => p.seller) // Force EF Core to load Seller details
    .ToListAsync();

    products = products.TakeLast(10).ToList();
return Ok(products);
    }
}
