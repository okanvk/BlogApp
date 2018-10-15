using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BlogApp.Data;
using BlogApp.Entity;
using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace BlogApp.UI.Controllers
{
  [Authorize]
  public class CommentController : Controller
  {
    private ICommentRepository repositorycomment;

    public CommentController(ICommentRepository repositorycomment)
    {
      this.repositorycomment=repositorycomment;
    }



    public IActionResult Index(int id)
    {     

      var comments=repositorycomment.GetAll().Where(x =>x.BlogId==id);      
      return View(comments);
    }

    public IActionResult Delete(int id){
      repositorycomment.DeleteComment(id);
      return RedirectToAction("List","Blog");
    }

    public IActionResult Approve(int id){
        var comment=repositorycomment.GetById(id);
        comment.isApproved=true;
        repositorycomment.UpdateComment(comment);
        return RedirectToAction("List","Blog");
    }
  }
}