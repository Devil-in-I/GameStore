using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService gameService;
        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        // GET: api/games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameModel>>> Get()
        {
            return Ok(await gameService.GetAllAsync());
        }

        // GET: api/games/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GameModel>> GetById(int id)
        {
            try
            {
                return Ok(await gameService.GetByIdAsync(id));
            }
            catch (ArgumentNullException)
            {

                return NotFound();
            }
        }

        // POST: api/games
        [HttpPost]
        public async Task<ActionResult<Game>> Add([FromBody] GameModel value)
        {
            if (string.IsNullOrEmpty(value.Name))
            {
                return BadRequest();
            }

            try
            {
                await gameService.AddAsync(value);
                return CreatedAtAction(nameof(Add), value);
            }
            catch (ArgumentNullException)
            {

                return BadRequest();
            }
        }

        // PUT: api/games/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<GameModel>> Update([FromBody] GameModel value, int id)
        {
            if (value is null)
            {
                return BadRequest();
            }
            else if (string.IsNullOrEmpty(value.Name))
            {
                return BadRequest();
            }

            try
            {
                var gameToUpdate = await gameService.UpdateAsync(id, value);
                return Ok(gameToUpdate);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }

        }

        // DELETE: api/games/{id}
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await gameService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentNullException)
            {
                return NoContent();
            }
        }

        [HttpPost("{gameId}/genres")]
        public async Task<ActionResult<GameModel>> AddGenreToGame(int gameId, GameGenreModel genre)
        {
            var game = await gameService.GetByIdAsync(gameId);

            if (game == null)
            {
                return NotFound();
            }

            game.Genres ??= new List<GameGenreModel>();

            // Game already has that genre
            if (game.Genres.Any(x => x.Name == genre.Name))
            {
                return BadRequest();
            }

            await gameService.AddGenreToGameAsync(gameId, genre);

            return CreatedAtAction("AddGenreToGame", new { id = game.Id }, game);
        }
    }
}
