using MiniRedSocial.Core.Application.Dtos.Account;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<UpdateResponse> UpdateUserAsync(UpdateRequest request);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();
        Task<string> GetUserByUsernameAsync(string username);

    }
}
