using System;
using innobyte_e_commerce.Models;

namespace E_commerce.Models;

public class CartItemsView
{

    public long ProductID{get;set;}

    public int Quantity{get;set;}

    public double Total{get;set;}
    public Product Product{get;set;}
    
}
