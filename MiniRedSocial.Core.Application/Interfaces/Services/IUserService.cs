using MiniRedSocial.Core.Application.Dtos.Account;
using MiniRedSocial.Core.Application.ViewModels.User;
using System.Security.Claims;

namespace MiniRedSocial.Core.Application.Interfaces.Services
{
    public interface IUserService 
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<UpdateResponse> UpdateAsync(SaveUserViewModel vm, string id);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();
    }
}
