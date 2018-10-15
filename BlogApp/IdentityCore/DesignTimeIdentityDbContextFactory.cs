using System;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BlogApp.IdentityCore{

  public class DesignTimeIdentityDbContextFactory : IDesignTimeDbContextFactory<ApplicationIdentityDbContext>
  {
    

    public ApplicationIdentityDbContext CreateDbContext(string[] args)
    {
      
       IConfigurationRoot configuration =new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .Build();

       var builder = new DbContextOptionsBuilder<ApplicationIdentityDbContext>();
       var connectionstring = configuration.GetConnectionString("IdentityConnection");
       builder.UseSqlServer(connectionstring,b => b.MigrationsAssembly("BlogApp.UI"));
       return new ApplicationIdentityDbContext(builder.Options);



    }
  }
}