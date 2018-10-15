using System;
using System.Linq;
using BlogApp.Entity;
using BlogApp.Data.Abstract;

namespace BlogApp.Data.Concrete.EFCore{

  public class EfCommentRepository : ICommentRepository
  {

     private BlogContext context;

      public EfCommentRepository(BlogContext _context)
      {
          this.context=_context;
      }

    public void AddComment(Comment entity)
    {
      entity.Date=DateTime.Now;
      context.Comments.Add(entity);
      context.SaveChanges();
    }

    public void DeleteComment(int CommentId)
    {
        var Comment = context.Comments.FirstOrDefault(c => c.CommentId==CommentId);
        context.Comments.Remove(Comment);
        context.SaveChanges();


    }

    public IQueryable<Comment> GetAll()
    {
      return context.Comments;
    }

    public Comment GetById(int CommentId)
    {
      return context.Comments.FirstOrDefault(x => x.CommentId==CommentId);
    }

    public void UpdateComment(Comment Entity)
    {
      var comment = GetById(Entity.CommentId);

      if(comment!=null){
        comment.isApproved=Entity.isApproved;
        context.SaveChanges();
      }
    }
  }


}

