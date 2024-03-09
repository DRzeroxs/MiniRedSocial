using AutoMapper;
using Microsoft.AspNetCore.Http;
using MiniRedSocial.Core.Application.Dtos.Account;
using MiniRedSocial.Core.Application.Helpers;
using MiniRedSocial.Core.Application.Interfaces.Repositories;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.Core.Application.ViewModels.Message;
using MiniRedSocial.Core.Application.ViewModels.Publication;
using MiniRedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.Services
{
    public class PublicationService : GenericService<SavePublicationViewModel, PublicationViewModel, Publication>, IPublicationService
    {
        private readonly IPublicationRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public PublicationService(IPublicationRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(repository, mapper)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SavePublicationViewModel> Add(SavePublicationViewModel vm)
        {
            vm.UserId = userViewModel.Id;
            return await base.Add(vm);
        }

        public async Task<List<PublicationViewModel>> GetAllViewModelByUserId(string userId)
        {

            var filteredEntities = await _repository.GetByUserIdAsync(userId);
            var orderedPublications = filteredEntities.OrderByDescending(x => x.Date).ToList();

            var vmPublicaciones = _mapper.Map<List<PublicationViewModel>>(orderedPublications);

            foreach (var vm in vmPublicaciones)
            {
                vm.UserImg = await _repository.GetUserImgUrlbyId(vm.UserId);
            }

            return vmPublicaciones;
        }

        public async Task<PublicationViewModel> GetViewModelById(int id)
        {
            var publication = await _repository.GetByIdAsync(id);

            var vmPublication = _mapper.Map<PublicationViewModel>(publication);

            vmPublication.UserImg = await _repository.GetUserImgUrlbyId(vmPublication.UserId);

            return vmPublication;
        }
    }
}
