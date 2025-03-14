using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection
{
    internal class Keyboard : IInput
    {
        private Printer prompter;
        public Keyboard(Printer prompter)
        {
            this.prompter = prompter;
        }
        public string GetStringInput()
        {
            return (Console.ReadLine() ?? "").Trim();
        }
        public string GetStringInput(string prompt)
        {
            prompter.PrintPrompt(prompt);
            return GetStringInput();
        }
        public int GetIntInput()
        {
            if (int.TryParse(GetStringInput(), out int result))
                return result;
            else
                return -1;
        }
        public int GetIntInput(string prompt)
        {
            prompter.PrintPrompt(prompt);
            return GetIntInput();
        }
    }
}
