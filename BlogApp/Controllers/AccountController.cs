using System;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Authorization;
using BlogApp.UI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BlogApp.IdentityCore;

namespace BlogApp.UI.Controllers{


[Authorize]
public class AccountController : Controller
  {
    private UserManager<ApplicationUser> userManager;
    private SignInManager<ApplicationUser> signInManager;

    private IPasswordHasher<ApplicationUser> passwordHasher;

    private IPasswordValidator<ApplicationUser> passwordValidator;



    public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IPasswordHasher<ApplicationUser> passwordHasher,IPasswordValidator<ApplicationUser> passwordValidator){
      this.userManager=userManager;
      this.signInManager=signInManager;
      this.passwordHasher=passwordHasher;
      this.passwordValidator=passwordValidator;
      
    }

    

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(){
    
      return View();
    }

  [HttpPost]
  [AllowAnonymous]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Login(LoginModel model){

    if(ModelState.IsValid){

      var user =await userManager.FindByEmailAsync(model.Email);

      if(user!=null){

        await signInManager.SignOutAsync();
        var result = await signInManager.PasswordSignInAsync(user,model.Password,false,false);

        if(result.Succeeded){
          return RedirectToAction("Index","Account");
        } 
      }
      ModelState.AddModelError(nameof(model.Email),"Hatalı kullanıcı adı yada parola");
    }

    return View(model);
  }


  public IActionResult List(){
      return View(userManager.Users);
    }
  
  public async Task<IActionResult> Logout(){
    
    await signInManager.SignOutAsync();
    return RedirectToAction("Index","Home");

  }

  [HttpGet]
  public IActionResult Register(){
    return View();
  }


  public async  Task<IActionResult> Delete(string ıd){
    var user =await userManager.FindByIdAsync(ıd);
    if(user!=null){
      var result =await userManager.DeleteAsync(user);
    }
     return RedirectToAction("List","Account");
  }


  [HttpPost]
  public async Task<IActionResult> Register(LoginModel lm){

      if(ModelState.IsValid){
      ApplicationUser user = new ApplicationUser();
      user.UserName=Guid.NewGuid().ToString();
      user.Email=lm.Email;
      user.Name=lm.Name;
      
      

      
      IdentityResult result = await userManager.CreateAsync(user,lm.Password);
      if(result.Succeeded){
        await userManager.AddToRoleAsync(user,"admin");
        
      }
      else{
        foreach(var item in result.Errors){
          ModelState.AddModelError("",item.Description);
          return View(lm);
        } 
      }
       RedirectToAction("List","Account"); 
    }
    
      return View(lm);
      
    }

  [HttpGet] 
  public async Task<IActionResult> Update(string id){

    var user =await userManager.FindByIdAsync(id);
    return View(user);
  }

  [HttpPost]
  public async Task<IActionResult> Update(string Id,string Password,string Email){
    
    var user = await userManager.FindByIdAsync(Id);

    if(string.IsNullOrWhiteSpace(Password)){
      ModelState.AddModelError("","Password required field");
      return View(user);
    }

    if(user!=null){

      user.Email=Email;

      IdentityResult validPass=null;

      if(!string.IsNullOrEmpty(Password)){
        validPass = await passwordValidator.ValidateAsync(userManager,user,Password);

        if(validPass.Succeeded){
          user.PasswordHash=passwordHasher.HashPassword(user,Password);
        }
        else{
          foreach (var item in validPass.Errors)
          {
              ModelState.AddModelError("",item.Description);
          }
        }
      }
      if(validPass.Succeeded){
        var result = await userManager.UpdateAsync(user);

        if(result.Succeeded){
          return RedirectToAction("List");
        }
        else{
          foreach (var item in result.Errors)
          {
              ModelState.AddModelError("",item.Description);
          }
        }
      }
    }
    else{
      ModelState.AddModelError("","User not found");
    }

    return View(user);
  }

  public IActionResult Index(){
    return View();
  }

  }
}