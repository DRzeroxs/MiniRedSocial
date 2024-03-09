using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using MiniRedSocial.Core.Application.Dtos.Account;
using MiniRedSocial.Core.Application.Enums;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.infrastructure.Identity.Entities;
using System.Security.Claims;
using System.Text;

namespace MiniRedSocial.infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IFriendshipService _friendshipService;
        private readonly IEmailService _emailService;

        public AccountService(IFriendshipService friendshipService,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _friendshipService = friendshipService;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Email}";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName,
                ImageUrl = request.ImageUrl,
                PhoneNumber = request.PhoneNumber,

            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.BasicUser.ToString());
                var verificationUri = await SendVerificationEmailUrl(user, origin);
                await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this URL {verificationUri}",
                    Subject = "Confirm registration"
                });
                response.UserId = user.Id;
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            

            return response;
        }

        public async Task<UpdateResponse> UpdateUserAsync(UpdateRequest request)
        {
            UpdateResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"User with ID '{request.UserId}' not found.";
                return response;
            }

            //mapear UpdateRequest a ApplicationUser
            user.Name = request.Name;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.ImageUrl = request.ImageUrl;
            user.PhoneNumber = request.PhoneNumber;

            if (!string.IsNullOrEmpty(request.Password))
            {
                var changePasswordResult = _userManager.ChangePasswordAsync(user, user.PasswordHash,request.Password);
                if (!changePasswordResult.IsCompletedSuccessfully)
                {
                    response.HasError = true;
                    response.Error = $"An error occurred while changing the password for user with ID '{request.UserId}'.";
                    return response;
                }
            }

            
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                response.Message = $"User with ID '{request.UserId}' updated successfully.";
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred while updating the user with ID '{request.UserId}'.";
            }

            return response;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No accounts registered with this user";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            else
            {
                return $"An error occurred while confirming {user.Email}.";
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            var verificationUri = await SendForgotPasswordUrl(user, origin);

            await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account visiting this URL {verificationUri}",
                Subject = "reset password"
            });


            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset password";
                return response;
            }

            return response;
        }
        private async Task<string> SendVerificationEmailUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
        private async Task<string> SendForgotPasswordUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUri;
        }

        public async Task<string> GetUserByUsernameAsync (string username)
        {

            var friend = await _userManager.FindByNameAsync(username);



            if (friend != null)
            {
                return friend.Id;
            }
            else
            {
                return null; 
            }


        }
    }
}
