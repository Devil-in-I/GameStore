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
    public class GameGenreRepository : Repository<GameGenre>, IGameGenreRepository
    {
        private readonly GameStoreDbContext context;

        public GameGenreRepository(GameStoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<GameGenre>> GetAllWithDetailsAsync()
        {
            return await context.Genres.Include(x => x.Games).ToListAsync();
        }

        public async Task<GameGenre> GetWithDetailsByIdAsync(int id)
        {
            return await context.Genres.Include(x => x.Games).SingleAsync(x => x.Id == id);
        }
    }
}
