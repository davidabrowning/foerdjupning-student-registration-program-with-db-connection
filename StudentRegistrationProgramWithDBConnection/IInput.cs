using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection
{
    internal interface IInput
    {
        string GetStringInput();
        string GetStringInput(string prompt);
        int GetIntInput();
        int GetIntInput(string prompt);
    }
}
