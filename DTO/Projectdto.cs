using System;
using System.ComponentModel.DataAnnotations;
using innobyte_e_commerce.Models;

namespace innobyte_e_commerce.DTO;

public class Projectdto
{

}

public class UserRegister{
    [Required]
    [MaxLength(30)]
    public string FirstName{get;set;}
    [Required]
    [MaxLength(30)]
    public string LastName{get;set;}
    [Required]
    [EmailAddress]
    public string Email{get;set;}
    
     [Required(ErrorMessage = "Password is required")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&]).{8,20}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
    public string Password{get;set;}= string.Empty;

    
   [Required(ErrorMessage = "Mobile number is required")]
    [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Invalid mobile number. It must be a 10-digit number starting with 6, 7, 8, or 9.")]
    public long Mobile{get;set;}

}
public class ProductsCategoryView{
    public List<Product> Products{get;set;}
    public List<string> Categories{get;set;}
}


public class View_Admin_ordersList
{
   

    
}
public class View_Admin_orders{
    public MasterOrder MasterOrder{get;set;}
    public List<Order> Orders{get;set;}
   
   
}
public class Address{
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public string Email{get;set;}
    public string Street{get;set;}
    public string City{get;set;}
    public string State{get;set;}
    
    public string Country{get;set;}
    public string Zipcode{get;set;}
    public string Mobile{get;set;}
    
}
public class OrderDetail
{
    public int OrderID { get; set; }
    public Product Product { get; set; }
    public MasterOrder MasterOrder { get; set; }
    public string UserName{get;set;}
}

public class UserLogin()
{
    [Required]
    [EmailAddress]
    public string UserEmail{get;set;}

    [Required]
    public string Password{get;set;}
}
public class uploadproduct
{
    public string ProductName{get;set;}
public string Description{get;set;}
public double Price{get;set;}
public int Stock{get;set;}
public long SellerId{get;set;}

public string ImageUrl{get;set;}
public string category{get;set;}
}