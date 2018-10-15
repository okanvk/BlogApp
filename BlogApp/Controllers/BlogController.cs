using System;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.UI.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlogApp.UI.Controllers{

public class BlogController : Controller
{

  private readonly IHostingEnvironment he;
  private IBlogRepository repositoryblog;
  private ICategoryRepository repositorycategory;

  private ICommentRepository repositorycomment;
  public BlogController(IBlogRepository repositoryblog,ICategoryRepository repositorycategory,ICommentRepository repositorycomment,IHostingEnvironment he)
  {
      this.repositoryblog=repositoryblog;
      this.repositorycategory=repositorycategory;
      this.repositorycomment=repositorycomment;
      this.he=he;
  }
  public IActionResult Index(int? id,string q)
  {

    var query =repositoryblog.GetAll()
    .Where(i => i.isApproved);

    if(id!=null){
      query=query
      .Where(i => i.CategoryId==id);
      
    }

    if(!string.IsNullOrEmpty(q)){
      query=query.Where(i => i.Title.Contains(q) || i.Description.Contains(q) || i.Body.Contains(q));
    }

    return View(query.OrderByDescending(i => i.Date));
   
  }


  [HttpGet]
  public IActionResult Details(int id){

    BlogCommentsModel bcm=new BlogCommentsModel();

    bcm.Blog=repositoryblog.GetById(id);
    bcm.Comments=repositoryblog.GetComments(bcm.Blog).Where(x =>x.isApproved).ToList();
    bcm.Comment=new Comment(bcm.Blog.BlogId);

    
    return View(bcm);
  }

  [HttpPost]

  public IActionResult Details(Comment comment){



      if(ModelState.IsValid){
        repositorycomment.AddComment(comment);
        return RedirectToAction("Details",comment.BlogId);
      }
      else{
         BlogCommentsModel bcm=new BlogCommentsModel();

        bcm.Blog=repositoryblog.GetById(comment.BlogId);
        bcm.Comments=repositoryblog.GetComments(bcm.Blog).Where(x =>x.isApproved).ToList();
        bcm.Comment=new Comment(bcm.Blog.BlogId);
        return View(bcm);
      }
  }

 public IActionResult CountIncrease(int id){

    var blog =repositoryblog.GetById(id); //Düzenlenecek AJAX ile
    blog.ViewCoun=blog.ViewCoun + 1;
    repositoryblog.UpdateBlog(blog);
    return Json(true);//Paket yolladık.
 }

    [Authorize]
    public IActionResult List(){
    return View(repositoryblog.GetAll());
  }


  [HttpGet,Authorize]
  public IActionResult Add(){
    ViewBag.Categories=new SelectList(repositorycategory.GetAll(),"CategoryId","Name");
    return View();
  }

  [HttpPost,Authorize]
  public IActionResult Add(Blog entity,IFormFile pic){

        if(ModelState.IsValid){
        string filename=Path.Combine(he.WebRootPath,Path.GetFileName(pic.FileName));
        string photo =filename.Split('/').Last();
        pic.CopyTo(new FileStream("wwwroot/img/"+photo,FileMode.Create));
        entity.Date=DateTime.Now;
        entity.Image=photo;
        repositoryblog.AddBlog(entity);
        return RedirectToAction("List");  
        }
      else
        ViewBag.Categories=new SelectList(repositorycategory.GetAll(),"CategoryId","Name");
        return View(entity);
      
    
  }


  [HttpGet,Authorize]
  public IActionResult Edit(int id){
    ViewBag.Categories=new SelectList(repositorycategory.GetAll(),"CategoryId","Name");
    return View(repositoryblog.GetById(id));
  }

  
  [HttpPost,Authorize]
  public async Task<IActionResult> Edit(Blog entity,IFormFile pic){


    if(ModelState.IsValid){

      if(pic!=null){
      string filename=Path.Combine(he.WebRootPath,Path.GetFileName(pic.FileName));
      string photo =filename.Split('/').Last();
      await pic.CopyToAsync(new FileStream("wwwroot/img/"+photo,FileMode.Create));
      entity.Image=photo;
      }

      repositoryblog.UpdateBlog(entity);
      TempData["message"]=$"{entity.Title} güncellendi";
      return RedirectToAction("List");

    }
    else{
      ViewBag.Categories=new SelectList(repositorycategory.GetAll(),"CategoryId","Name");
      return View(entity); 
    }
    //}
    /*ViewBag.Categories=new SelectList(repositorycategory.GetAll(),"CategoryId","Name");
    return View(entity);*/

  }


  [HttpGet,Authorize]
  public IActionResult Delete(int id){
    return View(repositoryblog.GetById(id));
  }


  [HttpPost,ActionName("Delete"),Authorize]
  public IActionResult DeleteConfirmed(int BlogId){
    repositoryblog.DeleteBlog(BlogId);
    TempData["message"]=$"{BlogId} numaralı kayıt silindi";
    return RedirectToAction("List");
  }

  
}
}