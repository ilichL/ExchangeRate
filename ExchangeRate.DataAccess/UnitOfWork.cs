using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Data;
using ExchangeRate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICurrencyRepository _repository;
        private readonly Context context;
        private readonly IRepository<Source> sourceRepository;
        public IRepository<Comment> commentsRepository { get; }

        public IRepository<Role> rolesRepository { get; }

        public IRepository<User> userRepository { get; }
        public ICurrencyRepository currencyRepository { get; }
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public UnitOfWork(ICurrencyRepository productRepository, Context context,
            IRepository<User> users,
            IRepository<Comment> comments,
            IRepository<Role> roles,
            ICurrencyRepository currencyRepository,
            IRepository<Source> sourceRepository,
            IRepository<UserRole> _userRoleRepository,
            IRepository<RefreshToken> refreshTokenRepository)
        {
            this.context = context;
            _repository = productRepository;
            commentsRepository = comments;
            rolesRepository = roles;
            userRepository = users;
            this.currencyRepository = currencyRepository;
            this.sourceRepository = sourceRepository;
            this._userRoleRepository = _userRoleRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }


        public IRepository<User> Users => userRepository;
        //Реалищовать эти репозитории 
        public IRepository<Comment> Comments => commentsRepository;

        public IRepository<Role> Roles => rolesRepository;


        public IRepository<Source> Sources => sourceRepository;

        public ICurrencyRepository Currencies => currencyRepository;
        public IRepository<UserRole> UserRoles => _userRoleRepository;
        public IRepository<RefreshToken> RefreshTokens => _refreshTokenRepository;

        async Task<int> IUnitOfWork.Save()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context?.Dispose();
            _repository.Dispose();
            GC.SuppressFinalize(this);//пометка на то, что объект можно удалять(нужно почистить оперативную память)
        }
    }
}
