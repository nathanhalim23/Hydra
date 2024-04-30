using HydraWeb.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace HydraWeb;

public class AccountController : Controller
{   
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }


    public IActionResult LoginPage(){
        return View("LoginIndex");
    }


    [HttpPost]
    public async Task<IActionResult> Login(UserLoginViewModel viewModel){
        if(ModelState.IsValid){
            try{
                var ticket = _service.SetLogin(viewModel);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ticket.Principal,
                    ticket.Properties
                );

                return RedirectToAction("Index", "Dashboard");
            }
            catch(Exception e){
                ViewBag.Message = e.Message;
            }
        }

        return View("LoginIndex");
    }

}
