using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string? NormalizedEmail { get; set; }

        public string? PasswordHash { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string[] RoleNames { get; set; }
    }
}
