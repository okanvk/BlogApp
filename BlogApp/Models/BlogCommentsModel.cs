using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using BlogApp.Entity;

namespace BlogApp.UI.Models
{
  public class BlogCommentsModel
  {

    public Blog Blog { get; set; }
    public List<Comment> Comments{get;set;}

    public Comment Comment {get;set;}
  }

}
