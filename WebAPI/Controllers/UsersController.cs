using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel model)
        {
            try
            {
                var result = await _userService.RegisterUserAsync(model.User, model.Password);
                if (result.Succeeded)
                {
                    return Ok("Registration successful");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (UserExistsException)
            {

                return BadRequest("User already exists.");
            }

        }

        [HttpPost("register-admin")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRegistrationModel model)
        {
            try
            {
                var result = await _userService.RegisterAdminUserAsync(model.User, model.Password);
                if (result.Succeeded)
                {
                    return Ok("Registration successful");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (UserExistsException)
            {

                return BadRequest("User already exists.");
            }

        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            try
            {
                var jwtToken = await _userService.LoginAsync(model.UserName, model.Password);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return Ok("Logout successful");
        }

        [HttpPut("current")]
        [Authorize]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var tokenObj = handler.ReadJwtToken(token);
            string? userId = tokenObj.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userToUpdate = await _userService.GetUserByIdAsync(userId);
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(userToUpdate, user);
                return Ok(updatedUser);
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var user = await _userService.GetUserByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userService.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok("Password changed successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("current")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var tokenObj = handler.ReadJwtToken(token);
            string? userId = tokenObj.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);

        }

        #region Actions to get games by user id
        // Couldn't put this logic into games controller

        [HttpGet("current/games")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GameModel>>> GetGamesByUserId()
        {
            try
            {
                string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var tokenObj = handler.ReadJwtToken(token);
                string? userId = tokenObj.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest();
                }
                var games = await _userService.GetGamesByUserIdAsync(userId);
                return Ok(games);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"EXCEPTION - {e.Message} | SOURCE - {e.Source}");
                await Console.Out.WriteLineAsync("\n\n    Stack trace: \n");
                await Console.Out.WriteLineAsync($"{e.StackTrace}");
                return BadRequest();
            }
        }

        [HttpPut("current/games/add/{gameId}")]
        [Authorize]
        public async Task<ActionResult<User>> AddGameByIdToUser([FromRoute]int gameId)
        {
            try
            {
                string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var tokenObj = handler.ReadJwtToken(token);
                string? userId = tokenObj.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User not found");
                }
                var user = await _userService.AddGameToUser(userId, gameId);
                return Ok(user);
            }
            catch (InvalidOperationException e)
            {
                await Console.Out.WriteLineAsync($"EXCEPTION - {e.Message} | SOURCE - {e.Source}");
                await Console.Out.WriteLineAsync("\n\n    Stack trace: \n");
                await Console.Out.WriteLineAsync($"{e.StackTrace}");
                return BadRequest("Game not found");
            }
            catch(Exception e)
            {
                await Console.Out.WriteLineAsync($"EXCEPTION - {e.Message} | SOURCE - {e.Source}");
                await Console.Out.WriteLineAsync("\n\n    Stack trace: \n");
                await Console.Out.WriteLineAsync($"{e.StackTrace}");
                return BadRequest(e.Message);
            }
        }
        #endregion
    }
}