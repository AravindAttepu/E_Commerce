using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace innobyte_e_commerce.Models;

public class UserVerify
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int VerifyID{get;set;}
    
    [ForeignKey("user")]
    public int UserID{get;set;}
    public User user{get;set;}
    public string EMailVerifiedToken{get;set;}
    
    public string MobileVerifiedToken{get;set;}

}
