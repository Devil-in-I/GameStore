using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager,
                           RoleManager<IdentityRole> roleManager,
                           SignInManager<User> signInManager,
                           IUnitOfWork unitOfWork,
                           IJwtTokenService jwtTokenService,
                           IConfiguration configuration,
                           IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            var userExists = await _userManager.FindByNameAsync(user.UserName);
            if (userExists != null)
            {
                throw new UserExistsException("user already exists!");
            }
            User userToRegister = user;
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> RegisterAdminUserAsync(User user, string password)
        {
            var userExists = await _userManager.FindByNameAsync(user.UserName);
            if (userExists != null)
            {
                throw new UserExistsException("user already exists!");
            }
            User userToRegister = user;
            var result = await _userManager.CreateAsync(user, password);

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                // Creating "Admin" role if not exists
                var adminRole = new IdentityRole("Admin");
                await _roleManager.CreateAsync(adminRole);
            }
            else if (!await _roleManager.RoleExistsAsync("User"))
            {
                // Creating "User" role if not exists
                var userRole = new IdentityRole("User");
                await _roleManager.CreateAsync(userRole);
            }

            // Присваиваем пользователю роль "Admin"
            await _userManager.AddToRoleAsync(user, "Admin");

            return result;
        }

        public async Task<JwtSecurityToken> LoginAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var token = _jwtTokenService.GenerateToken(authClaims);
                return token;
            }
            throw new UnauthorizedException("user is unauthorized");
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<IEnumerable<GameModel>> GetGamesByUserIdAsync(string userId)
        {
            var games = await _unitOfWork.Game.GetGamesByUser(userId);
            return _mapper.Map<IEnumerable<GameModel>>(games);
        }

        public async Task<User> AddGameToUser(string userId, int gameId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            var game = await _unitOfWork.Game.GetWithDetailsByIdAsync(gameId);
            if (game == null)
            {
                throw new InvalidOperationException("Game not found");
            }

            user.Games ??= new List<Game>();
            user.Games.Add(game);

            await _userManager.UpdateAsync(user);

            return user;
        }

        public async Task<User> UpdateUserAsync(User userToUpdate, User updatedUser)
        {
            if (userToUpdate == null)
            {
                throw new InvalidOperationException("User not found");
            }

            // Обновление значений пользователя
            userToUpdate.UserName = updatedUser.UserName;
            userToUpdate.Email = updatedUser.Email;
            // Другие свойства пользователя, которые вы хотите обновить

            var result = await _userManager.UpdateAsync(userToUpdate);

            if (!result.Succeeded)
            {
                // Обработка ошибок при обновлении пользователя
                throw new InvalidOperationException("Failed to update user");
            }

            return userToUpdate;
        }

    }
}
