using System;
using System.Linq;
using BlogApp.Entity;
using BlogApp.Data.Abstract;

namespace BlogApp.Data.Concrete.EFCore{

  public  class EfCategoryRepository : ICategoryRepository{

      private BlogContext context;

      public EfCategoryRepository(BlogContext _context)
      {
          this.context=_context;
      }

    public void AddCategory(Category entity)
    {
       context.Categories.Add(entity);
       context.SaveChanges();
    }

    public void DeleteCategory(int CategoryId)
    {
      var Blog = context.Categories.FirstOrDefault(x => x.CategoryId==CategoryId);
      if(Blog!=null){
        context.Categories.Remove(Blog);
        context.SaveChanges();
      }
    }

    public IQueryable<Category> GetAll()
    {
      return context.Categories;
    }

    public Category GetById(int CategoryId)
    {
       return context.Categories.FirstOrDefault(b => b.CategoryId==CategoryId);
    }

    public void UpdateCategory(Category entity)
    {
      context.Entry(entity).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
      context.SaveChanges();
    }
  }
}



