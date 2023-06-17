using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(User user, string password);
        Task<IdentityResult> RegisterAdminUserAsync(User user, string password);
        Task<JwtSecurityToken> LoginAsync(string userName, string password);
        Task LogoutAsync();
        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
        Task<User> UpdateUserAsync(User userToUpdate, User updatedUser);
        Task<User> GetUserByEmailAsync(string email); // Добавленный метод
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task<IEnumerable<GameModel>> GetGamesByUserIdAsync(string userId);
        Task<User> AddGameToUser(string userId, int gameId);
    }
}