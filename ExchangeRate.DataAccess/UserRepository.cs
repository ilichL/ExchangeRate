using ExchangeRate.Data;
using ExchangeRate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.DataAccess
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(Context context) : base(context)
        {
        }
    }
}
