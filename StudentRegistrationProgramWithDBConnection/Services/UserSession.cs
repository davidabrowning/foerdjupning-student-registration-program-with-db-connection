using Part2RegistrationProgramWithDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Part2RegistrationProgramWithDB.Services
{
    internal static class UserSession
    {
        public static SystemUser? SystemUser { get; set; }
        public static bool IsLoggedIn => SystemUser != null;
    }
}
