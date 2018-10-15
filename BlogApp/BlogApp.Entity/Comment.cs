using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace BlogApp.Entity
{
    
    public class Comment{

      public Comment(int BlogId)
      {
          this.BlogId=BlogId;
          
      }

      public Comment(){}
      public int CommentId { get; set; }

      public int BlogId{get;set;}

      public Blog Blog{get;set;}

      [Required(ErrorMessage="Lütfen isminizi giriniz")]          
      public string PublisherName { get; set; }

      [Required(ErrorMessage="Lütfen içeriğinizi giriniz")]
      public string Body{get;set;}
      
      [BindNever]
      public DateTime Date{get;set;}

      public bool isApproved { get; set; }


      
    }

}