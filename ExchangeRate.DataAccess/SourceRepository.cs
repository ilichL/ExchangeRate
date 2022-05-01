using ExchangeRate.Data;
using ExchangeRate.Data.Entities;

namespace ExchangeRate.DataAccess
{
    public class SourceRepository : Repository<Source>
    {
        public SourceRepository(Context context) : base(context)
        {
        }
    }
}
