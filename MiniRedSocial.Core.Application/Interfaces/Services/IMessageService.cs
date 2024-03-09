using MiniRedSocial.Core.Application.ViewModels.Message;
using MiniRedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.Interfaces.Services
{
    public interface IMessageService : IGenericService<SaveMessageViewModel, MessageViewModel, Message>
    {
        Task<List<MessageViewModel>> GetAllViewModelByPublicationId(int publicationId);

        Task<MessageViewModel> GetViewModelById(int id);
    }
}
