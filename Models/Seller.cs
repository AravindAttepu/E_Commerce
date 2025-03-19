using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace innobyte_e_commerce.Models;

public class Seller
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public long SellerId{get;set;}
[Required]
public string SellerName{get;set;}
   [DefaultValue(0)]
public double SellerRating{get;set;}
}
