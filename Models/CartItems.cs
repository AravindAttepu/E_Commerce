using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace innobyte_e_commerce.Models;

public class CartItems
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartItemID{get;set;}

    [ForeignKey("Cart")]
    public int CartID{get;set;}

    [ForeignKey("Product")]
    public long ProductID{get;set;}

    public int Quantity{get;set;}

    public double Total{get;set;}

     public Cart Cart { get; set; }

    public Product Product { get; set; }

}
