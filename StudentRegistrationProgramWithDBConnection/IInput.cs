using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection
{
    internal interface IInput
    {
        public string GetStringInput();
        public string GetStringInput(string prompt);
        public int GetIntInput();
        public int GetIntInput(string prompt);
    }
}
