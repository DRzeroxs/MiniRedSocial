using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.Core.Application.ViewModels.Message;
using MiniRedSocial.Core.Application.ViewModels.Publication;
using MiniRedSocial.infrastructure.Identity.Entities;

namespace MiniRedSocial.Controllers
{
    public class MessageController : Controller
    {
        private readonly IPublicationService _publicationService;
        private readonly IMessageService _messageService;
        private readonly IHiloService _hiloService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController (IMessageService messageService, IHiloService hiloService, UserManager<ApplicationUser> userManager, IPublicationService publicationService)
        {
            _messageService = messageService;
            _hiloService = hiloService;
            _userManager = userManager;
            _publicationService = publicationService;
        }

        [HttpGet]
        public async Task<IActionResult> VerComentarios(int id)
        {
            PublicationViewModel vm = await _publicationService.GetViewModelById(id);

            vm.Messages = await _messageService.GetAllViewModelByPublicationId(vm.Id);

            foreach (var message in vm.Messages)
            {
                message.Respuestas = await _hiloService.GetAllViewModelByPublicationId(message.Id);
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult AgregarComentario(int id)
        {
            SaveMessageViewModel saveVm = new SaveMessageViewModel();

            saveVm.PublicationId = id;

            return View(saveVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarComentario(SaveMessageViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            vm.Id = 0;

            vm.Date = DateTime.Now;

            await _messageService.Add(vm);

            return RedirectToRoute(new { controller = "Message", action = "VerComentarios", id = vm.PublicationId });
        }
    }
}
