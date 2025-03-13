using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection
{
    internal class Keyboard
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
    }
}
