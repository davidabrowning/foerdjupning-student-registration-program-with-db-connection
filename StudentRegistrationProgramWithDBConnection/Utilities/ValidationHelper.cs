using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection.Utilities
{
    internal static class ValidationHelper
    {
        public static bool IsValidStringInput(string input)
        {
            return !string.IsNullOrEmpty(input);
        }
        public static bool IsValidIntInput(string input)
        {
            return int.TryParse(input, out int result);
        }
    }
}
