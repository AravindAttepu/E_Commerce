using System;
using innobyte_e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace innobyte_e_commerce.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options ):base(options)
    {

    }
    public DbSet<User> Users{get;set;}
    public DbSet<Order> Orders{get;set;}
    public DbSet<MasterOrder> MasterOrders{get;set;}
    public DbSet<Cart> Carts{get;set;}
    public DbSet<CartItems> CartItems{get;set;}
    public DbSet<Seller> Sellers{get;set;}
    public DbSet<Product> products{get;set;}
    public DbSet<UserVerify> UserVerification{get;set;}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // When a user is deleted, delete related orders
  

    // No Cascade Delete for Product (Prevents Multiple Cascade Paths)
    modelBuilder.Entity<Order>()
        .HasOne(o => o.Product)
        .WithMany()
        .HasForeignKey(o => o.ProductID)
        .OnDelete(DeleteBehavior.NoAction);

    // No Cascade Delete for Master Order
    modelBuilder.Entity<Order>()
        .HasOne(o => o.MasterOrder)
        .WithMany()
        .HasForeignKey(o => o.MasterID)
        .OnDelete(DeleteBehavior.NoAction);
}
}