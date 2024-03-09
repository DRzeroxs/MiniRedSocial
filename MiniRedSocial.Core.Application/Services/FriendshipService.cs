using AutoMapper;
using Microsoft.AspNetCore.Http;
using MiniRedSocial.Core.Application.Dtos.Account;
using MiniRedSocial.Core.Application.Helpers;
using MiniRedSocial.Core.Application.Interfaces.Repositories;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.Core.Application.ViewModels.Friendship;
using MiniRedSocial.Core.Domain.Entities;

namespace MiniRedSocial.Core.Application.Services
{
    public class FriendshipService : GenericService<SaveFriendshipViewModel, FriendshipViewModel, Friendship>, IFriendshipService
    {
        private readonly IFriendshipRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public FriendshipService(IFriendshipRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(repository, mapper)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SaveFriendshipViewModel> Add(SaveFriendshipViewModel vm)
        {
            vm.UserId = userViewModel.Id;
            return await base.Add(vm);
        }

        public async Task<List<FriendshipViewModel>> GetFriendshipsByUserId(string userId)
        {
            var friendships = await _repository.GetAllAsync(); 

            // Filtrar las amistades para obtener solo las del usuario actual
            var userFriendships = friendships.Where(f => f.UserId == userId || f.FriendId == userId).ToList();

            var userFriendshipViewModels = _mapper.Map<List<FriendshipViewModel>>(userFriendships);

            return userFriendshipViewModels;
        }

        public async Task<bool> AreFriends(string usedId, string friendId)
        {
            var friendship = await _repository.GetFriendshipByUserIds(usedId, friendId);

            return friendship != null;
        }
    }
}
