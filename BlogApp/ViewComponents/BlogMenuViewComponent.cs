using System;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Data.Abstract;
using System.Linq;


namespace BlogApp.UI.ViewComponents{
   public class BlogMenuViewComponent :ViewComponent{


    private IBlogRepository repository;

    public BlogMenuViewComponent(IBlogRepository repository)
    {
        this.repository=repository;
    }
    public IViewComponentResult Invoke(){
      return View(repository.GetAll()
      .Where(x => x.isApproved)
      .OrderByDescending(x => x.ViewCoun)
      .Take(5)  
      );
    }



  }
}