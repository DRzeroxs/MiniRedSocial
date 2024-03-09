using AutoMapper;
using MiniRedSocial.Core.Application.Dtos.Account;
using MiniRedSocial.Core.Application.Helpers;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.Core.Application.ViewModels.User;

namespace MiniRedSocial.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper) 
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            
            RegisterResponse result = await _accountService.RegisterBasicUserAsync(registerRequest, origin);

            if(!result.HasError && result.UserId != null) 
            {
                UpdateRequest update = _mapper.Map<UpdateRequest>(registerRequest);
                update.UserId = result.UserId;
                update.ImageUrl = FileManager.UploadFile(vm.File, result.UserId);
                await _accountService.UpdateUserAsync(update);
            }

            return result;
        }

        public async Task<UpdateResponse> UpdateAsync(SaveUserViewModel vm, string id)
        {
            UpdateRequest update = _mapper.Map<UpdateRequest>(vm);

            if(vm.File != null)
            {
                update.ImageUrl = FileManager.UploadFile(vm.File, id);
            }
            
            update.UserId = id;
            return await _accountService.UpdateUserAsync(update);
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(resetRequest);
        }


        
    }
}
