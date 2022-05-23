using ExchangeRate.Data;
using ExchangeRate.Data.Entities;

namespace ExchangeRate.DataAccess
{
    public class RefreshTokenRepository : Repository<RefreshToken>
    {
        public RefreshTokenRepository(Context context) : base(context)
        {
        }
    }
}
