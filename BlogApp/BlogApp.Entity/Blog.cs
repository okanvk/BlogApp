using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Entity
{
    public class Blog{

      public int BlogId { get; set; }


      [Required(ErrorMessage="Lütfen başlık giriniz")]
      public string Title { get; set; }
      [Required(ErrorMessage="Lütfen açıklama giriniz")]
      public string Description { get; set; }
      [Required(ErrorMessage="Lütfen içerik giriniz")]
      public string Body { get; set; }

      [BindNever] //Form üzerinden almıyoruz veriyi...
      public DateTime Date {get;set;}

      public string Image {get;set;}
      public bool isApproved { get; set; }

      public bool isHome {get;set;}

      public bool isSlider { get; set; }

      public int CategoryId { get; set; }
      public Category Category{get;set;}

      public int ViewCoun{get;set;}

      public List<Comment> Comments{get;set;}

    }
}