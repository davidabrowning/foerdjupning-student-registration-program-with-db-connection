using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2RegistrationProgramWithDB.Models
{
    internal class UserRole
    {
        public int UserRoleId { get; set; } = 0;
        public string RoleName { get; set; } = "";
        public bool CanAddStudent { get; set; } = false;
        public bool CanReadStudent { get; set; } = false;
        public bool CanUpdateStudent { get; set; } = false;
        public bool CanDeleteStudent { get; set; } = false;
    }
}
