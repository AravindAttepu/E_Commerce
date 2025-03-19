using System;
using E_commerce.Models;
using innobyte_e_commerce.Models;

namespace innobyte_e_commerce.Models;

public class UserViewModel
{
  public User UserDetails { get; set; }
        public List<CartItemsView> CartProducts { get; set; }
        public List<CartItemsView> UserOrders { get; set; }
        
        public double GrandTotal{get;set;}
}
