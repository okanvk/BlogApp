using System;
using System.Collections.Generic;
using System.Linq;
using BlogApp.Entity;

namespace BlogApp.Data.Abstract{

  public interface IBlogRepository
  {
      List<Blog> GetAll();

      Blog GetById(int BlogId);
 
      void AddBlog(Blog entity);

      void UpdateBlog(Blog entity);

      void DeleteBlog(int BlogId);

      IQueryable<Comment> GetComments(Blog entity);   
  }

}