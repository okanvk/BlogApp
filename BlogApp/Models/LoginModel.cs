using System;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Entity;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.UI.Models
{
  public class LoginModel
  {


    [Required]
    [UIHint("email")]
    public string Email { get; set; }

    [Required]
    [UIHint("password")]
    public string Password { get; set; }


    
    public string Name { get; set; }

  }

}
