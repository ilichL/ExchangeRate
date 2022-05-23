using AutoMapper;
using ExchangeRate.Core.Interfaces;
using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Data;
using ExchangeRate.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeRate.Core.DTOs;

namespace ExchangeRate.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleService _roleService;

        public AccountService(IMapper mapper,
            IUnitOfWork unitOfWork,
            IRoleService roleService,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roleService = roleService;
            _configuration = configuration;
        }

        public async Task<bool> CheckUserWithThatEmailIsExistAsync(string email)
        {
            var normalizedEmail = email.ToUpperInvariant();

            return await _unitOfWork.Users.Get()
                .AnyAsync(user =>
                    user.Email
                        .Equals(normalizedEmail));
        }

        public async Task<Guid?> GetUserIdByEmailAsync(string email)
        {
            var normalizedEmail = email.ToUpperInvariant();

            return (await (await _unitOfWork.Users.FindBy(user =>
                user.Email != null && user.Email
                    .Equals(normalizedEmail))).FirstOrDefaultAsync())?.ID;
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            return _mapper.Map<UserDTO>(await _unitOfWork.Users.GetById(id));

        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var normalizedEmail = email.ToUpperInvariant();

            var user = await _unitOfWork.Users
                .Get()
                .Where(user =>
                    user.NormalizedEmail != null && user.NormalizedEmail
                    .Equals(normalizedEmail))
                .Include(user => user.UserRoles)
                .ThenInclude(role => role.Role)
                .FirstOrDefaultAsync();

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var user = (await (await _unitOfWork.RefreshTokens
                .FindBy(token => token.Token.Equals(refreshToken)))
                .FirstOrDefaultAsync())
                .User;

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<Guid> CreateUserAsync(string modelName,string modelEmail)
        {
            var id = Guid.NewGuid();
            await _unitOfWork.Users.Add(new User()
            {
                ID = id,
                Email = modelEmail,
                //Name = modelName,
                NormalizedEmail = modelEmail.ToUpperInvariant(),
                RegistrationDate = DateTime.Now
            });
            await _unitOfWork.Save();
            return id;
        }

        public async Task<Guid> CreateUserAsync(string modelEmail)
        {
            var id = Guid.NewGuid();
            await _unitOfWork.Users.Add(new User()
            {
                ID = id,
                Email = modelEmail,
                NormalizedEmail = modelEmail.ToUpperInvariant(),
                RegistrationDate = DateTime.Now
            });
            await _unitOfWork.Save();
            return id;
        }

        public async Task<int> SetRoleAsync(Guid userId, string roleName)
        {
            var roleId = await _roleService.GetRoleIdByNameAsync(roleName);
            if (roleId == Guid.Empty)
            {
                roleId = await _roleService.CreateRole(roleName);
            }

            await _unitOfWork.UserRoles.Add(new UserRole
            {
                ID = Guid.NewGuid(),
                UserId = userId,
                RoleId = roleId
            });
            return await _unitOfWork.Save();
        }

        public async Task<IEnumerable<string>> GetRolesAsync(Guid userId)
        {
            var userRoleIds = (await _unitOfWork
                .Users.GetByIdWithIncludes(userId,
                        user => user.UserRoles))
                .UserRoles.Select(role => role.RoleId);

            var names = new List<string>();
            foreach (var userRoleId in userRoleIds)
            {
                names.Add(await _roleService.GetRoleNameByIdAsync(userRoleId));
            }

            return names;
        }

        public async Task<int> SetPasswordAsync(Guid userId, string password)
        {

            await _unitOfWork.Users.СhangeAsync(userId, new List<PatchModel>
            {
                    new PatchModel()
                    {
                        PropertyName = "PasswordHash",
                        PropertyValue = GetPasswordHash(password,_configuration["ApplicationVariables:Salt"])
                    }
                });
            return await _unitOfWork.Save();

        }

        public async Task<bool> CheckPassword(string email, string password)
        {
            var userId = await GetUserIdByEmailAsync(email);
            if (userId.GetValueOrDefault() != Guid.Empty)
            {
                var userPasswordHash = (await _unitOfWork.Users.GetById(userId.GetValueOrDefault())).PasswordHash;
                if (!string.IsNullOrEmpty(userPasswordHash))
                {
                    var enteredPasswordHash = GetPasswordHash(password, _configuration["ApplicationVariables:Salt"]);

                    if (userPasswordHash.Equals(enteredPasswordHash))
                        return true;
                }
            }
            return false;
        }

        private string GetPasswordHash(string password, string salt)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1Data = sha1.ComputeHash(Encoding.UTF8.GetBytes($"{salt}_{password}"));
            var hashedPassword = Encoding.UTF8.GetString(sha1Data);
            return hashedPassword;
        }
    }
}

