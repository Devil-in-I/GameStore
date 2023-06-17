using Data.Interfaces;
using Data.Repositories;

namespace Data.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDbContext context;
        private IGameRepository gameRepository;
        private IGameGenreRepository gameGenreRepository;

        public UnitOfWork(GameStoreDbContext context)
        {
            this.context = context;
        }

        public IGameRepository Game => gameRepository ??= new GameRepository(context);

        public IGameGenreRepository GameGenre => gameGenreRepository ??= new GameGenreRepository(context);

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
