using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2RegistrationProgramWithDB.Models
{
    internal class SystemUser
    {
        public int SystemUserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public override string? ToString()
        {
            return $"{Username} ({UserRole.RoleName})";
        }
    }
}
