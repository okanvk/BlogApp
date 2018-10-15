using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogApp.Entity
{
  public class  Message{ 
    public int MessageId { get; set; }

    [Required(ErrorMessage="Lütfen isminizi giriniz")]
    public string Name { get; set; }

    [Required(ErrorMessage="Lütfen Emailinizi giriniz"),EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage="Lütfen içerik giriniz")]
    public string Context { get; set; }

    [Required(ErrorMessage="Lütfen konu giriniz")]
    public string Subject{get;set;}

    public bool isRead { get; set; }

    [BindNever] //Form üzerinden almıyoruz veriyi...
      public DateTime Date {get;set;}

      
  }
}