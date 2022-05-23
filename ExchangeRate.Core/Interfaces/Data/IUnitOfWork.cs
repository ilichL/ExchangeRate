using ExchangeRate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable//заставляет импользовать метод Dispose
    {//реализация интерфейса в DataAccess
        ICurrencyRepository Currencies { get; }
        public IRepository<Comment> Comments { get; }

        public IRepository<Role> Roles { get; }

        public IRepository<Source> Sources { get; }
        public IRepository<User> Users { get; }
        public IRepository<UserRole> UserRoles { get; }
        public IRepository<RefreshToken> RefreshTokens { get; }

        public Task<int> Save();

    }
}
