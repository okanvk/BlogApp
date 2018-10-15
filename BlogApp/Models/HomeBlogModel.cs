using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using BlogApp.Entity;

namespace BlogApp.UI.Models
{
  public class HomeBlogModel
  {

    public List<Blog> SliderBlogs { get; set; }
    public List<Blog> HomeBlogs{get;set;}
  }

}
