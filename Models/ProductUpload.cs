using System;

namespace innobyte_e_commerce.Models;

public class ProductUpload
{
 public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string category{get;set;}
    public long SellerId{get;set;}
    public int Stock{get;set;}
    public IFormFile Image { get; set; }
}
