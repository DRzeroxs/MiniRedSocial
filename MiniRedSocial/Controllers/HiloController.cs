using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.Core.Application.ViewModels.Hilo;
using MiniRedSocial.Core.Application.ViewModels.Message;
using MiniRedSocial.infrastructure.Identity.Entities;

namespace MiniRedSocial.Controllers
{
    public class HiloController : Controller
    {

        private readonly IPublicationService _publicationService;
        private readonly IMessageService _messageService;
        private readonly IHiloService _hiloService;
        private readonly UserManager<ApplicationUser> _userManager;

        public HiloController(IMessageService messageService, IHiloService hiloService, UserManager<ApplicationUser> userManager, IPublicationService publicationService)
        {
            _messageService = messageService;
            _hiloService = hiloService;
            _userManager = userManager;
            _publicationService = publicationService;
        }

        [HttpGet]
        public async Task<IActionResult> VerRespuestas(int id)
        {
            MessageViewModel vm = await _messageService.GetViewModelById(id);

            if(vm == null)
            {
                return NotFound();
            }

            vm.Respuestas = await _hiloService.GetAllViewModelByPublicationId(id);

            return View(vm);
        }

        [HttpGet]
        public IActionResult AgregarRespuesta(int id)
        {
            SaveHiloViewModel saveVm = new SaveHiloViewModel();

            saveVm.MessageId = id;

            return View(saveVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarRespuesta(SaveHiloViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            vm.Id = 0;

            vm.Date = DateTime.Now;

            await _hiloService.Add(vm);

            return RedirectToRoute(new { controller = "Hilo", action = "VerRespuestas", id = vm.MessageId });
        }
    }
}
