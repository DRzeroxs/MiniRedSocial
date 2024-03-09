using MiniRedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.Interfaces.Repositories
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        Task<List<Message>> GetByPublicationIdAsync(int publicationId);
        Task<string> GetUserImgUrlbyId(string id);
    }
}
