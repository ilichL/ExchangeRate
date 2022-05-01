using ExchangeRate.Data;
using ExchangeRate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.DataAccess
{
    public class CommentRepository : Repository<Comment>
    {
        public CommentRepository(Context context) : base(context)
        {
        }
    }
}
