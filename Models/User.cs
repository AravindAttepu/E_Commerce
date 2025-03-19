using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace innobyte_e_commerce.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID{get;set;}
    [Required]
    [MaxLength(30)]
    public string Name{get; set;}
    [Required]
    [EmailAddress]
    public string Email{get;set;}
    [Required]
    public string PasswordHash{get;set;}
    [Required]
    public long Mobile{get;set;}
    public bool EMailVerified{get;set;}
    public bool MobileVerified{get;set;}
   

}
