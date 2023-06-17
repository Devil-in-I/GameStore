using Business.Models;

namespace Business.Interfaces
{
    public interface IGameService : ICrudService<GameModel>
    {
        Task AddGenreToGameAsync(int gameId, GameGenreModel genre);
    }
}
