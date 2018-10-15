using System;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace BlogApp.UI.Controllers{

public class CategoryController : Controller
{
  private ICategoryRepository repositorycategory;

  private IBlogRepository repositoryblog;
  public CategoryController(ICategoryRepository repositorycategory,IBlogRepository repositoryblog)
  {
      this.repositorycategory=repositorycategory;
      this.repositoryblog=repositoryblog;
  }

  [Authorize]
  public IActionResult List(){
      return View(repositorycategory.GetAll());
    }

  [HttpGet,Authorize]
  public IActionResult Add(){
    return View();
  } 

  [HttpPost,Authorize]
  public IActionResult Add(Category entity){

      if(ModelState.IsValid){
      repositorycategory.AddCategory(entity);
      return RedirectToAction("List");
      }
      else
      return View(entity);
  }



    [HttpGet,Authorize]
    public IActionResult Update(int id){

      var category=repositorycategory.GetById(id);
      return View(category);

    } 

    [HttpPost,Authorize]
    public IActionResult Update(Category entity){

      if(ModelState.IsValid){
        repositorycategory.UpdateCategory(entity);
        return RedirectToAction("List");
      }
      else
      return View(entity);

    }

    public IActionResult Delete(int id){
      
      var staticID=3004;
      
      var category=repositorycategory.GetById(staticID);
      var blogs=repositoryblog.GetAll().Where(x => x.CategoryId==id);

      if(blogs!=null){

        foreach(var item in blogs){
        item.CategoryId=category.CategoryId;
        repositoryblog.UpdateBlog(item);
        }

      }
      

      repositorycategory.DeleteCategory(id);      
      return RedirectToAction("List");

    }
  
  
  
  }
}