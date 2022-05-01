using AutoMapper;
using ExchangeRate.Core.DTOs;
using ExchangeRate.Core.Interfaces;
using ExchangeRate.Core.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRate.Domain.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<CommentsDTO>> GetAllCommentsAsync()
        {
            return await unitOfWork.Comments.Get().
                Select(comment => mapper.Map<CommentsDTO>(comment)).
                ToArrayAsync();
        }
        public async Task<int> DeleteAsync(Guid id)
        {
            unitOfWork.Comments.Delete(id);
            return await unitOfWork.Save();
        }
    }
}
