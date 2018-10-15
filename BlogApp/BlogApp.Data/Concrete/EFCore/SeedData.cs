using System;
using Microsoft.EntityFrameworkCore;
using BlogApp.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BlogApp.Data.Concrete.EFCore{

  public static class SeedData{


    public static void Seed(IApplicationBuilder app){

      BlogContext context=app.ApplicationServices.GetRequiredService<BlogContext>();

      context.Database.Migrate();

     
      if(!context.Messages.Any()){

        context.Messages.AddRange(
          new Message(){Name="deneme1",Subject="s"}
        );
        context.SaveChanges();
      }

      if(!context.Categories.Any()){
          //Category olmadan blog eklenemez çünkü her blog kategoride bulunmalı
        context.Categories.AddRange(

          new Category(){Name="Category1"},
          new Category(){Name="Category2"},
          new Category(){Name="Category3"}
        );
        context.SaveChanges();
      }


   



    }
  }

}