using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace innobyte_e_commerce.Models;

public class Cart
{
     [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int  CartID{get;set;}
    [ForeignKey("user")]
    public int UserID{get;set; }

    public double GrandTotal{get;set;}
     public User user { get; set; }
    public ICollection<CartItems> CartItems { get; set; }

}
