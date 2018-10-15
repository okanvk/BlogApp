using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.UI.Models;

namespace BlogApp.UI.Controllers{

  public class HomeController : Controller
  {

    private IBlogRepository blogRepository;
    private IMessageRepository messageRepository;

    public HomeController(IBlogRepository blogRepository,IMessageRepository messageRepository)
    {
        this.blogRepository=blogRepository;
        this.messageRepository = messageRepository;
    }
    public IActionResult Index()
    {
      HomeBlogModel model=new HomeBlogModel();
      model.HomeBlogs=blogRepository.GetAll().Where(i => i.isApproved && i.isHome).OrderByDescending(x => x.Date).ToList();
      model.SliderBlogs=blogRepository.GetAll().Where(i => i.isSlider).ToList();

      return View(model);
    }


    [HttpGet]
    public IActionResult Contact(){
      
      ViewBag.state=false;
      return View();
    }

    [HttpPost]
    public IActionResult Contact(Message entity){

      
      
      if(ModelState.IsValid){
        entity.Date=DateTime.Now;
        ViewBag.state=messageRepository.AddMessage(entity);      
        return View();
      }
      else{
        ViewBag.state=false;
        return View(entity);
      }
      
    }

    public IActionResult About(){
      return View();
    }
   

   
  }


}