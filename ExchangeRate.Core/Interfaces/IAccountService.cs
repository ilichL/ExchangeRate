using ExchangeRate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CheckUserWithThatEmailIsExistAsync(string email);
        Task<Guid> CreateUserAsync(string modelEmail);
        Task<int> SetRoleAsync(Guid userId, string roleName);
        Task<IEnumerable<string>> GetRolesAsync(Guid userId);
        Task<int> SetPasswordAsync(Guid userId, string password);
        Task<Guid?> GetUserIdByEmailAsync(string email);
        Task<UserDTO> GetUserById(Guid id);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<UserDTO> GetUserByRefreshTokenAsync(string refreshToken);
        Task<bool> CheckPassword(string email, string password);
        Task<Guid> CreateUserAsync(string modelEmail, string modelName);
    }
}
