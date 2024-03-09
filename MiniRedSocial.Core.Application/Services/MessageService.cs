using AutoMapper;
using Microsoft.AspNetCore.Http;
using MiniRedSocial.Core.Application.Dtos.Account;
using MiniRedSocial.Core.Application.Helpers;
using MiniRedSocial.Core.Application.Interfaces.Repositories;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.Core.Application.ViewModels.Message;
using MiniRedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.Services
{
    public class MessageService : GenericService<SaveMessageViewModel, MessageViewModel, Message>, IMessageService
    {
        private readonly IMessageRepository _repository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(repository, mapper)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SaveMessageViewModel> Add(SaveMessageViewModel vm)
        {
            vm.UserId = userViewModel.Id;
            return await base.Add(vm);
        }

        public async Task<List<MessageViewModel>> GetAllViewModelByPublicationId( int publicationId)
        {
            
            var mensajes = await _repository.GetByPublicationIdAsync(publicationId);

            var vmMensajes = _mapper.Map<List<MessageViewModel>>(mensajes);

            foreach ( var vm in vmMensajes)
            {
                vm.UserImg = await _repository.GetUserImgUrlbyId(vm.UserId);
            }

            return vmMensajes;
        }

        public async Task<MessageViewModel> GetViewModelById(int id)
        {
            var message = await _repository.GetByIdAsync(id);

            var vmMessage = _mapper.Map<MessageViewModel>(message);

            vmMessage.UserImg = await _repository.GetUserImgUrlbyId(vmMessage.UserId);

            return vmMessage;
        }

    }
}
