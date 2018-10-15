using System;
using System.Linq;
using BlogApp.Entity;

namespace BlogApp.Data.Abstract{

  public interface ICategoryRepository
  {
      
      Category GetById(int CategoryId);
      IQueryable<Category> GetAll();

      void AddCategory(Category entity);

      void UpdateCategory(Category entity);

      void DeleteCategory(int CategoryId);



  }

}