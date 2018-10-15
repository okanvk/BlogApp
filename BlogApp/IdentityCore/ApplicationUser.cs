using System;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.IdentityCore{

  public class ApplicationUser:IdentityUser{

    public string Name { get; set; }

   
    
  }
}