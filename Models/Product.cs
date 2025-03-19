using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace innobyte_e_commerce.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public long ProductId{get;set;}
[Required]
[MaxLength(300)]
public string ProductName{get;set;}
public string Description{get;set;}
[Required]
public double Price{get;set;}
[Required]
public int Stock{get;set;}
[Required]
[ForeignKey("seller")]
public long SellerId{get;set;}
[Required]
public string ImageUrl{get;set;}
public string category{get;set;}
List<productImage> ProductImages{get;set;}

public Seller seller{get;set;}

    
}
