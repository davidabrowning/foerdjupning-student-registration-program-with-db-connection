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
        public int GetIntInput(string prompt)
        {
            prompter.PrintPrompt(prompt);
            if (int.TryParse(prompt, out int result))
                return result;
            else 
                return -1;
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
