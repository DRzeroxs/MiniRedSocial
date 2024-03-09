using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.Core.Application.ViewModels.Publication;
using MiniRedSocial.Core.Application.ViewModels.User;
using MiniRedSocial.infrastructure.Identity.Entities;

namespace MiniRedSocial.Controllers
{
    [Authorize(Roles = "BasicUser")]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPublicationService _publicationService;
        private readonly IMessageService _messageService;
        private readonly IHiloService _hiloService;

        public HomeController(IUserService userService, IPublicationService publicationService, IMessageService messageService,IHiloService hiloService, UserManager<ApplicationUser> userManager)
        { 
            _publicationService = publicationService;
            _messageService = messageService;
            _hiloService = hiloService;
            _userManager = userManager;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            List<PublicationViewModel> publicasiones = await _publicationService.GetAllViewModelByUserId(user.Id);

            foreach (var publication in publicasiones)
            {
                publication.Messages = await _messageService.GetAllViewModelByPublicationId(publication.Id);

                foreach(var message in publication.Messages)
                {
                    message.Respuestas = await _hiloService.GetAllViewModelByPublicationId(message.Id);
                }
            }

            return View(publicasiones);
        }


        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new SaveUserViewModel
            {
                Name = user.Name,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                ImageUrl = user.ImageUrl,
                Password = user.PasswordHash,
                Email = user.Email

            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new SaveUserViewModel
            {
                Name = user.Name,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                ImageUrl = user.ImageUrl,
                Password = user.PasswordHash,
                Email = user.Email

            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(SaveUserViewModel vm)
        {
            var user = await _userManager.GetUserAsync(User);


            if (user == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }


            vm.ImageUrl = user.ImageUrl;
            await _userService.UpdateAsync(vm, user.Id);

            return RedirectToAction(nameof(Profile));
        }

    }
}
