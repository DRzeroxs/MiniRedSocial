using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.Core.Application.ViewModels.Friendship;
using MiniRedSocial.Core.Application.ViewModels.Publication;
using MiniRedSocial.infrastructure.Identity.Entities;

namespace MiniRedSocial.Controllers
{
    public class FriendShipController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPublicationService _publicationService;
        private readonly IAccountService _accountService;
        private readonly IMessageService _messageService;
        private readonly IFriendshipService _friendshipService;
        private readonly IHiloService _hiloService;

        public FriendShipController(IAccountService accountService,IFriendshipService friendshipService,IUserService userService, IPublicationService publicationService, IMessageService messageService, IHiloService hiloService, UserManager<ApplicationUser> userManager)
        {
            _publicationService = publicationService;
            _accountService = accountService;
            _messageService = messageService;
            _friendshipService = friendshipService;
            _hiloService = hiloService;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            // Obtener las amistades del usuario actual
            List<FriendshipViewModel> userFriendships = await _friendshipService.GetFriendshipsByUserId(user.Id);

            // Obtener los IDs de amigos del usuario actual
            List<string> friendIds = userFriendships.Select(f => f.FriendId == user.Id ? f.UserId : f.FriendId).ToList();

            List<PublicationViewModel> publicaciones = await _publicationService.GetAllViewModel();

            List<PublicationViewModel> publicacionesFiltradas = publicaciones
                .Where(publicacion => friendIds.Contains(publicacion.UserId))
                .ToList();

            foreach (var publicacion in publicacionesFiltradas)
            {
                publicacion.Messages = await _messageService.GetAllViewModelByPublicationId(publicacion.Id);

                foreach (var mensaje in publicacion.Messages)
                {
                    mensaje.Respuestas = await _hiloService.GetAllViewModelByPublicationId(mensaje.Id);
                }
            }

            return View(publicacionesFiltradas);
        }

        [HttpGet]
        public IActionResult AddFriend()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend(string username)
        {
            var CurrentUser = await _userManager.GetUserAsync(User);

            string FriendUser = await _accountService.GetUserByUsernameAsync(username);

            if (FriendUser == null)
            {
                ModelState.AddModelError(string.Empty, $"No se encontró ningún usuario con el nombre de usuario '{username}'.");
                return View();
            }

            var areFriends = await _friendshipService.AreFriends(CurrentUser.Id, FriendUser);

            if (areFriends)
            {
                return Conflict();
            }

            var frienship = new SaveFriendshipViewModel
            {
                UserId = CurrentUser.Id,
                FriendId = FriendUser
            };

            await _friendshipService.Add(frienship);

            return RedirectToAction("Index", "FriendShip");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFriend(int friendshipId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            await _friendshipService.Delete(friendshipId);

            return RedirectToAction("Index", "FriendShip");
        }

    }
}
