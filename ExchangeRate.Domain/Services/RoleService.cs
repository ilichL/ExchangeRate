using AutoMapper;
using ExchangeRate.Core.Interfaces;
using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRate.Domain.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> GetRoleIdByNameAsync(string name)
        {
            var id = await (await _unitOfWork.Roles
                    .FindBy(role => role.Name.Equals(name)))
                .Select(role => role.ID)
                .FirstOrDefaultAsync();
            return id;
        }

        public async Task<string> GetRoleNameByIdAsync(Guid id)
        {
            return (await _unitOfWork.Roles.GetById(id)).Name;
        }

        public async Task<Guid> CreateRole(string name)
        {
            var id = Guid.NewGuid();
            await _unitOfWork.Roles.Add(new Role()
            {
                ID = id,
                Name = name
            });
            await _unitOfWork.Save();
            return id;
        }
    }
}
