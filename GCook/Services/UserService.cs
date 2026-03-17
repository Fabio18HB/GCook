using System.Security.Claims;
using GCook.Helpers;
using GCook.Models;
using GCook.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GCook.Services;

public class UserService : IUserService
{
    private readonly SignInManager<Usuario> _signInManager;
    private readonly UserManager<Usuario> _userManager;
    private readonly ILogger<UserService> _logger;

    public UserService(SignInManager<Usuario> signInManager, 
    UserManager<Usuario> userManager, 
    ILogger<UserService> logger          
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<SignInResult> Login(LoginVM login)
    {
       string userName = login.Email;
       if (Helper.IsValidEmail(login.Email))
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user != null)
                userName = user.UserName;
        }

        var result = await _signInManager.PasswordSignInAsync(
            userName, login.Senha, login.Lembrar, lockoutOnFailure: true
        );

        if (result.Succeeded)
        _logger.LogInformation($"Usuario '{userName}' acesso o sistema");
        if (result.IsLockedOut)
        _logger.LogWarning($"Usuário '{userName}' foi bloqueado");

        return result;
    }

    public async Task Logout()
    {
        _logger.LogInformation($"Usuario '{ClaimTypes.Email}' saiu do sistema" );
        await _signInManager.SignOutAsync();
    }

}
