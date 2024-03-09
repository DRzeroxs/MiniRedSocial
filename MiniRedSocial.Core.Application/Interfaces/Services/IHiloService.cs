using MiniRedSocial.Core.Application.ViewModels.Hilo;
using MiniRedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.Interfaces.Services
{
    public interface IHiloService : IGenericService<SaveHiloViewModel, HiloViewModel, Hilo>
    {
        Task<List<HiloViewModel>> GetAllViewModelByPublicationId(int messageId);
    }
}
