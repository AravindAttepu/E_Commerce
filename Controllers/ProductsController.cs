using innobyte_e_commerce.Data;
using innobyte_e_commerce.DTO;
using innobyte_e_commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace innobyte_e_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _baseImageUrl;

          public ProductsController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _baseImageUrl = "http://e-commercedemo.runasp.net/images/";
        }
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        
        
      
     
      [HttpGet("getproducts")]
public async Task<IActionResult> getProducts(string Search=" ",int sellerid=0)
{
    IQueryable<Product> query = _context.products.Include(p => p.seller);
    if(sellerid > 0)
    {
        query= query.Where(p=> p.SellerId == sellerid);
    }

    if (!string.IsNullOrWhiteSpace(Search))
    {
        query = query.Where(p => (p.ProductName != null && p.ProductName.ToLower().Contains(Search.ToLower())) ||
                                 (p.Description != null && p.Description.ToLower().Contains(Search.ToLower())) ||
                                 (p.category != null && p.category.ToLower().Contains(Search.ToLower())));
    }

    // Execute the query in the database
    List<Product> list = await query.ToListAsync();

    // Get distinct categories
    List<string> categories = list.Select(l => l.category).Distinct().ToList();

    // Update ImageUrl for each product
    list.ForEach(p => p.ImageUrl = _baseImageUrl + p.ImageUrl);
    ProductsCategoryView productsCategoryView= new ProductsCategoryView{
        Products= list,
        Categories= categories
    };

    return Ok(productsCategoryView);
}


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductbyId(int id)
        {
            var productexists = await _context.products.FirstOrDefaultAsync(p => p.ProductId == id);
            if(productexists == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            
        productexists.ImageUrl= _baseImageUrl + productexists.ImageUrl;
            return View(productexists);

        }

        // public async Task<IActionResult> AddProducts()
        // {
            
        // }
   

         
        //  var a=  await _context.products.Select(p=>p).ToListAsync();
        //    return Ok(a);

        public IActionResult Index(String Search="")
        {
          return View(model: Search ?? "");
           
          
        }
//          <summary>
//   /// Uploads an image for a product and stores the image in the "Images" folder.
//         </summary>
       
      
    }

}
