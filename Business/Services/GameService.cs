using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(GameModel model)
        {
            var game = _mapper.Map<Game>(model);
            await _unitOfWork.Game.AddAsync(game);
            await _unitOfWork.Save();
        }

        public async Task DeleteAsync(int modelId)
        {
            var game = await _unitOfWork.Game.GetByIdAsync(modelId);
            if (game != null)
            {
                _unitOfWork.Game.Delete(game);
                await _unitOfWork.Save();
            }
        }

        public async Task<IEnumerable<GameModel>> GetAllAsync()
        {
            var games = await _unitOfWork.Game.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<GameModel>>(games);
        }

        public async Task<GameModel> GetByIdAsync(int id)
        {
            var game = await _unitOfWork.Game.GetWithDetailsByIdAsync(id);
            return _mapper.Map<GameModel>(game);
        }

        public async Task AddGenreToGameAsync(int gameId, GameGenreModel genre)
        {
            var existingGenre = (await _unitOfWork.GameGenre.GetAllAsync()).FirstOrDefault(g => g.Name == genre.Name);
            var game = await _unitOfWork.Game.GetWithDetailsByIdAsync(gameId);

            if (existingGenre != null)
            {
                // Genre is already exist. Adding existing genre to the game, without adding new genre to the table Genres. It will add new record to table Game_Genre in sql server.
                game.Genres ??= new List<GameGenre>();
                game.Genres.Add(existingGenre);
            }
            else
            {
                // Genre is completely new. Adding new genre to game and therefore to genres table.
                game.Genres ??= new List<GameGenre>();
                game.Genres.Add(_mapper.Map<GameGenre>(genre));
            }

            await _unitOfWork.Save();
        }

        public async Task<GameModel> UpdateAsync(int id, GameModel model)
        {
            var gameToUpdate = await _unitOfWork.Game.GetByIdAsync(id);
            gameToUpdate.Name = model.Name;
            gameToUpdate.Price = model.Price;
            gameToUpdate.Description = model.Description;
            _unitOfWork.Game.Update(gameToUpdate);
            await _unitOfWork.Save();
            return _mapper.Map<GameModel>(gameToUpdate);
        }
    }
}
