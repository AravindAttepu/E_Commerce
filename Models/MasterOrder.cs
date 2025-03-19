using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace innobyte_e_commerce.Models;

public class MasterOrder
{
 [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int MasterID{get; set;}

[ForeignKey("User")]
public int UserID{get;set;}
public double GrandTotal{get;set;}
public string Address{get;set;}
public string PaymentMethod{get;set;}
public DateOnly Date{get;set;}
public int TotalItems{get;set;}

public User User{get;set;}

}
