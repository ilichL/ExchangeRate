using ExchangeRate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.Interfaces
{
    public interface ICommentService
    {
        public Task<IEnumerable<CommentsDTO>> GetAllCommentsAsync();
        public Task<int> DeleteAsync(Guid id);
    }
}
