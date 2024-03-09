using AutoMapper;
using MiniRedSocial.Core.Application.Dtos.Account;
using MiniRedSocial.Core.Application.ViewModels.Friendship;
using MiniRedSocial.Core.Application.ViewModels.Hilo;
using MiniRedSocial.Core.Application.ViewModels.Message;
using MiniRedSocial.Core.Application.ViewModels.Publication;
using MiniRedSocial.Core.Application.ViewModels.User;
using MiniRedSocial.Core.Domain.Entities;

namespace MiniRedSocial.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region UserProfile

            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateRequest, RegisterRequest>()
                .ReverseMap()
                .ForMember(x => x.UserId, opt => opt.Ignore());

            CreateMap<UpdateRequest, SaveUserViewModel>()
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region MessageProfile
            CreateMap<Message, MessageViewModel>()
                .ForMember(x => x.Respuestas, opt => opt.Ignore())
                .ForMember(x => x.UserImg, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Publication, opt => opt.Ignore())
                .ForMember(x => x.Hilos, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore());

            CreateMap<Message, SaveMessageViewModel>()
                .ReverseMap()
                .ForMember(x => x.Publication, opt => opt.Ignore())
                .ForMember(x => x.Hilos, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore());
            #endregion

            #region HiloProfile
            CreateMap<Hilo, HiloViewModel>()
                .ForMember(x => x.UserImg, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Message, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore());

            CreateMap<Hilo, SaveHiloViewModel>()
                .ReverseMap()
                .ForMember(x => x.Message, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore());
            #endregion

            #region PublicationProfile
            CreateMap<Publication, PublicationViewModel>()
                .ForMember(x => x.Messages, opt => opt.Ignore())
                .ForMember(x => x.UserImg, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Messages, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore());

            CreateMap<Publication, SavePublicationViewModel>()
                .ForMember(x => x.File, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Messages, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore());
            #endregion

            #region FriendshipProfile
            CreateMap<Friendship, FriendshipViewModel>()
                .ForMember(x => x.Friends, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore());

            CreateMap<Friendship, SaveFriendshipViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore());
            #endregion
        }
    }
}
