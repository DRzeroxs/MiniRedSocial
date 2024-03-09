using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniRedSocial.Core.Application.Helpers;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.Core.Application.ViewModels.Publication;
using MiniRedSocial.infrastructure.Identity.Entities;

namespace MiniRedSocial.Controllers
{
    public class PublicationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPublicationService _publicationService;
        private readonly IMessageService _messageService;
        private readonly IHiloService _hiloService;

        public PublicationController (UserManager<ApplicationUser> userManager, IPublicationService publicationService, IMessageService messageService, IHiloService hiloService)
        {
            _userManager = userManager;
            _publicationService = publicationService;
            _messageService = messageService;
            _hiloService = hiloService;
        }

        [HttpGet]
        public IActionResult SelectType()
        {
            return View( new SelectTipeViewModel());
        }

        [HttpPost]
        public IActionResult SelectType(SelectTipeViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            SavePublicationViewModel viewModel = new SavePublicationViewModel();

            viewModel.Tipe = vm.Tipe;

            return RedirectToAction(nameof(CreatePublication), new { tipe = viewModel.Tipe });
        }

        [HttpGet]
        public IActionResult CreatePublication(int tipe)
        {
            SavePublicationViewModel viewModel = new SavePublicationViewModel();
            viewModel.Tipe = tipe;

            if (viewModel.Tipe <= 0 || viewModel.Tipe > 3)
            {
                return View(nameof(SelectType));
            }

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePublication(SavePublicationViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await _userManager.GetUserAsync(User);

            vm.UserId = user.Id;
            vm.Date = DateTime.Now;

            if(vm.Tipe == 3)
            {
                vm.Url = FileManager.GetEmbeddedLink(vm.Url);
            }

            SavePublicationViewModel publication = await _publicationService.Add(vm);

            if (publication.Tipe == 2)
            {
                if(publication != null && publication.Id != 0)
                {
                    publication.Url = FileManager.UploadFile(vm.File, publication.Id.ToString());

                    await _publicationService.Update(publication, publication.Id);
                }
                
            }


            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


        [HttpGet]
        public async Task<IActionResult> EliminarPublicacion(int id)
        {
            return View(await _publicationService.GetByIdSaveViewModel(id));
        }

        [HttpPost, ActionName("EliminarPublicacion")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {

            await _publicationService.Delete(id);
            string basePath = $"/Images/Publications/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
