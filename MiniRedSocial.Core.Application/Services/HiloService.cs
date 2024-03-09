using AutoMapper;
using Microsoft.AspNetCore.Http;
using MiniRedSocial.Core.Application.Dtos.Account;
using MiniRedSocial.Core.Application.Helpers;
using MiniRedSocial.Core.Application.Interfaces.Repositories;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.Core.Application.ViewModels.Hilo;
using MiniRedSocial.Core.Application.ViewModels.Message;
using MiniRedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.Services
{
    public class HiloService : GenericService<SaveHiloViewModel, HiloViewModel, Hilo>, IHiloService
    {
        private readonly IHiloRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public HiloService(IHiloRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(repository, mapper)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SaveHiloViewModel> Add(SaveHiloViewModel vm)
        {
            vm.UserId = userViewModel.Id;
            return await base.Add(vm);
        }

        public async Task<List<HiloViewModel>> GetAllViewModelByPublicationId(int messageId)
        {

            var hilos = await _repository.GetByMessageIdAsync(messageId);

            var vmHilos = _mapper.Map<List<HiloViewModel>>(hilos);

            foreach (var vm in vmHilos)
            {
                vm.UserImg = await _repository.GetUserImgUrlbyId(vm.UserId);
            }

            return vmHilos;
        }

    }
}
