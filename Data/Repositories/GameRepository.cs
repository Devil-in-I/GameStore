using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        private readonly GameStoreDbContext context;
        public GameRepository(GameStoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Game>> GetAllWithDetailsAsync()
        {
            return await context.Games.Include(x => x.Genres).ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByUser(string id)
        {
            var a = await context.Games.Include(x => x.Users).Include(x => x.Genres).ToListAsync();
            return a.Where(x => x.Users.Any(u => u.Id == id));
        }

        public async Task<Game> GetWithDetailsByIdAsync(int id)
        {
            return await context.Games.Include(x => x.Genres).SingleAsync(x => x.Id == id);
        }
    }
}
