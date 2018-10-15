using System;
using System.Linq;
using BlogApp.Entity;


namespace BlogApp.Data.Abstract{

  public interface IMessageRepository
  {
      IQueryable<Message> GetAll();

      Message GetById(int MessageId);
 
      bool AddMessage(Message entity);

      void DeleteMessage(int MessageId);

      void UpdateMessage(int MessageId);
  }
}