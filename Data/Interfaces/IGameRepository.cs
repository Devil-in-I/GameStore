using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game> GetWithDetailsByIdAsync(int id);
        Task<IEnumerable<Game>> GetAllWithDetailsAsync();
        Task<IEnumerable<Game>> GetGamesByUser(string id);
    }
}
