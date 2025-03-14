using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection
{
    internal interface IOutput
    {
        void PrintTitle(string text);
        void PrintMessage(string text);
        void PrintError(string text);
        void PrintWarning(string text);
        void PrintSuccess(string text);
        void PrintLine();
        void PrintPrompt(string text);
        void ConfirmToContinue();
        void PrintList<T>(IEnumerable<T> list);
        void Clear();
    }
}
