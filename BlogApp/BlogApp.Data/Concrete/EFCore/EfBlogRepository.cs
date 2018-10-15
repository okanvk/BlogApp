using System;
using System.Linq;
using BlogApp.Entity;
using BlogApp.Data.Abstract;
using System.Collections.Generic;

namespace BlogApp.Data.Concrete.EFCore{

  public  class EfBlogRepository : IBlogRepository{

      private BlogContext context;

      public EfBlogRepository(BlogContext _context)
      {
          this.context=_context;
      }

    public void AddBlog(Blog entity)
    {
      context.Blogs.Add(entity);
      context.SaveChanges();
    }

    public void DeleteBlog(int BlogId)
    {
      var Blog = context.Blogs.FirstOrDefault(x => x.BlogId==BlogId);
      if(Blog!=null){
        context.Blogs.Remove(Blog);
        context.SaveChanges();
      }
    }

    public List<Blog> GetAll()
    {
      return context.Blogs.ToList();
    }

    public Blog GetById(int BlogId)
    {
      return context.Blogs.FirstOrDefault(b => b.BlogId==BlogId);
    }

    public IQueryable<Comment> GetComments(Blog entity){

        return context.Comments.Where(x => x.BlogId==entity.BlogId);
    }

    public void UpdateBlog(Blog entity)
    {
      var Blog = GetById(entity.BlogId);

      if(Blog!=null){
        
        Blog.ViewCoun=entity.ViewCoun;
        Blog.isApproved=entity.isApproved;
        Blog.isHome=entity.isHome;
        Blog.Title=entity.Title;
        Blog.Body=entity.Body;
        Blog.CategoryId=entity.CategoryId;
        Blog.Image=entity.Image;
        Blog.Description=entity.Description;
        Blog.isSlider=entity.isSlider;

        context.SaveChanges();
      }
      
    }

   
  }

}



