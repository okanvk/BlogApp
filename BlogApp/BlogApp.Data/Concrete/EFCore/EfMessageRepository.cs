using System;
using System.Linq;
using BlogApp.Entity;
using BlogApp.Data.Abstract;

namespace BlogApp.Data.Concrete.EFCore{
  public  class EfMessageRepository : IMessageRepository{

      private BlogContext context;

      public EfMessageRepository(BlogContext _context)
      {
          this.context=_context;
      }

    public bool AddMessage(Message entity)
    {
        try
        {
        context.Messages.Add(entity);
        context.SaveChanges();
        return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    public void DeleteMessage(int MessageId)
    {
      var Message=context.Messages.FirstOrDefault(x => x.MessageId==MessageId);
      if(Message!=null){
        context.Messages.Remove(Message);
        context.SaveChanges();
      }
    }

    public IQueryable<Message> GetAll()
    {
      return context.Messages;
    }

    public Message GetById(int MessageId)
    {
      return context.Messages.FirstOrDefault(x => x.MessageId==MessageId);
    }

    public void UpdateMessage(int MessageId)
    {
      
      var Message=context.Messages.FirstOrDefault(x => x.MessageId==MessageId);
      Message.isRead=true;
      context.SaveChanges();

    }
  }
}