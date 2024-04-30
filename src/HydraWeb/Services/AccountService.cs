using System.Security.Claims;
using HydraBusiness;
using HydraWeb.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace HydraWeb;

public class AccountService
{
    private readonly IUserRepository _userRepository;

    public AccountService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    private ClaimsPrincipal GetPrincipal(UserLoginViewModel viewModel){
        var claims = new List<Claim>(){
            new Claim("username", viewModel.Username)
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        return new ClaimsPrincipal(identity);
    }


    private AuthenticationTicket GetTicket(ClaimsPrincipal principal){
        AuthenticationProperties authenticationProperties = new AuthenticationProperties(){
            IssuedUtc = DateTime.Now,
            ExpiresUtc = DateTime.Now.AddMinutes(30),
            AllowRefresh = false
        };

        AuthenticationTicket authenticationTicket = new AuthenticationTicket(
            principal, authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme
        );

        return authenticationTicket;
    }


    public AuthenticationTicket SetLogin(UserLoginViewModel viewModel){
        var model = _userRepository.GetUserByUsername(viewModel.Username);
        bool passwordIsMatch = BCrypt.Net.BCrypt.Verify(viewModel.Password, model?.Password);

        if(!passwordIsMatch){
            throw new UsernamePasswordException("Username or password is incorrect!");
        }
        else{
            viewModel = new UserLoginViewModel(){
                Username = model.Username,
                Password = model.Password
            };
            ClaimsPrincipal principal = GetPrincipal(viewModel);
            AuthenticationTicket ticket = GetTicket(principal);
            return ticket;
        }
        
    }

}
