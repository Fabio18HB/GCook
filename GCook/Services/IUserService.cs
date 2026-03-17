using GCook.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GCook.Services;

public interface IUserService
{
    Task<SignInResult> Login(LoginVM login);
    Task Logout();
}
