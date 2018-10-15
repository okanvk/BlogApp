using System;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Data.Abstract;


namespace BlogApp.UI.ViewComponents{
   public class CategoryMenuViewComponent :ViewComponent{


    private ICategoryRepository repository;

    public CategoryMenuViewComponent(ICategoryRepository repository)
    {
        this.repository=repository;
    }
    public IViewComponentResult Invoke(){

      ViewBag.SelectedCategory=RouteData?.Values["id"]; // null olmadığı durumlarda valueları almıyacağız.
      return View(repository.GetAll());
    }



  }
}