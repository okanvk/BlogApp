using System;
using System.Linq;
using BlogApp.Entity;

namespace BlogApp.Data.Abstract{

  public interface ICommentRepository
  {
      
      IQueryable<Comment> GetAll();

      void AddComment(Comment entity);

      void DeleteComment(int CommentId);

      Comment GetById(int CommentId);

      void UpdateComment(Comment Entity);

  }

}