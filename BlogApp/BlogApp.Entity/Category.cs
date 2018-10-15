using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlogApp.Entity
{
    
    public class Category{

      public int CategoryId { get; set; }

      [Required(ErrorMessage="Lütfen kategori isimi giriniz")]
      public string Name { get; set; }

      public List<Blog> blogs {get;set;}
      
  

    }

}