using MiniRedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.Interfaces.Repositories
{
    public interface IHiloRepository : IGenericRepository<Hilo>
    {
        Task<List<Hilo>> GetByMessageIdAsync(int messageId);

        Task<string> GetUserImgUrlbyId(string id);
    }
}
