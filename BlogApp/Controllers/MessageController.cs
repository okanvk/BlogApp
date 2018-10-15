using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;

namespace BlogApp.UI.Controllers
{
  public class MessageController : Controller
  {

   private IMessageRepository messageRepository;

    public MessageController(IMessageRepository messageRepository)
    {
      this.messageRepository = messageRepository;
    }


    [Authorize]
    public IActionResult Index()
    {
      return View(messageRepository.GetAll());
    }

    [Authorize]
    public IActionResult Delete(int id){

      messageRepository.DeleteMessage(id);
      return RedirectToAction("Index");

    }

    
    [HttpGet,Authorize]
    public IActionResult Details(int id){

      var message =messageRepository.GetById(id);
      message.isRead=true;
      messageRepository.UpdateMessage(id);
      return View(message);
  
    }

    [HttpPost,Authorize]
    public IActionResult Details(Message entity){


     var cls=messageRepository.GetById(entity.MessageId);
      var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
      client.UseDefaultCredentials = false;
      client.Credentials = new NetworkCredential("okanvk.ciftci@gmail.com", "123VUKAT12");
      client.EnableSsl = true;

        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("okanvk.ciftci@gmail.com");
        mailMessage.To.Add(cls.Email);
        mailMessage.Body = entity.Context;
        mailMessage.Subject = entity.Subject;

        client.Send(mailMessage);
       
       return RedirectToAction("Index");

      }

    

  }
}