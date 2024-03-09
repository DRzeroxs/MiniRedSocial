using MiniRedSocial.Core.Application.ViewModels.Publication;
using MiniRedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.Interfaces.Services
{
    public interface IPublicationService : IGenericService<SavePublicationViewModel, PublicationViewModel, Publication>
    {
        Task<List<PublicationViewModel>> GetAllViewModelByUserId(string userId);

        Task<PublicationViewModel> GetViewModelById(int id);
    }
}
