using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.IdentityCore{

  public static class SeedIdentity{

    public static async Task CreateIdentityUsers(IServiceProvider serviceprovider,IConfiguration configuration){

        var userManager=serviceprovider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceprovider.GetRequiredService<RoleManager<IdentityRole>>();
        
        var username=configuration["Data:AdminUser:username"];
        var email=configuration["Data:AdminUser:email"];
        var password=configuration["Data:AdminUser:password"];
        var role=configuration["Data:AdminUser:role"];

        if(await userManager.FindByNameAsync(username)==null){ //Null ise veritabanında yoktur eklenmistir yeniden ekleme gerek yok  

          if(await roleManager.FindByNameAsync(role)==null){ //bos iserole de eklenecektir

            await roleManager.CreateAsync(new IdentityRole(role));
            
          }

          ApplicationUser user =new ApplicationUser(){
            UserName=username,
            Email=email,
            Name="okan"
          };

          IdentityResult result= await userManager.CreateAsync(user,password);

          if(result.Succeeded){
            await userManager.AddToRoleAsync(user,role);
          }

        }
    }


  }
}