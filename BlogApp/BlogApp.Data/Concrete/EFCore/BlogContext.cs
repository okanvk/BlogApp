using System;
using Microsoft.EntityFrameworkCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete.EFCore{

  public class BlogContext:DbContext{


    public BlogContext(DbContextOptions<BlogContext> options)
      : base(options)
    {
        
    }
    public DbSet<Blog>  Blogs { get; set; }
    public DbSet<Category> Categories {get;set;}
    public DbSet<Message> Messages {get;set;}
    public DbSet<Comment> Comments{get;set;}

  }

}