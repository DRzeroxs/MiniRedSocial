using MiniRedSocial.Core.Application.Dtos.Account;
using MiniRedSocial.Core.Application.Helpers;
using MiniRedSocial.Core.Application.ViewModels.User;

namespace MiniRedSocial.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            if (userViewModel == null)
            {
                return false;
            }
            return true;
        }
    }
}
