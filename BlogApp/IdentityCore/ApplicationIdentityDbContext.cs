using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.IdentityCore{

  public class ApplicationIdentityDbContext:IdentityDbContext<ApplicationUser>{

    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
    : base(options)
    {
        
    }


  }
}