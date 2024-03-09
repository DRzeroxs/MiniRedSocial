using MiniRedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.Interfaces.Repositories
{
    public interface IPublicationRepository : IGenericRepository<Publication>
    {
        Task<List<Publication>> GetByUserIdAsync(string userId);

        Task<string> GetUserImgUrlbyId(string id);

        Task<Publication> GetViewModelByIdAsync(int id);
    }
}
