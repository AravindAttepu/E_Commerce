using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace innobyte_e_commerce.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        // Foreign Key for Product (No Cascade Delete to Avoid Multiple Cascade Paths)
        [ForeignKey("Product")]
        public long ProductID { get; set; }
        public Product Product { get; set; }

        public User User { get; set; }
    
       [ForeignKey("User")]
        public int UserID { get; set; }
        

        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

        // Foreign Key for Master Order (No Cascade Delete)
        [ForeignKey("MasterOrder")]
        public int MasterID { get; set; }
        public MasterOrder MasterOrder { get; set; }
    }
}
