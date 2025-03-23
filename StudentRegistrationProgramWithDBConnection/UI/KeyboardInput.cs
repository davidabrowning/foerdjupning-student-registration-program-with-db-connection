using StudentRegistrationProgramWithDBConnection.Interfaces;
using StudentRegistrationProgramWithDBConnection.Utilities;

namespace StudentRegistrationProgramWithDBConnection.UI
{
    internal class KeyboardInput : IInput
    {
        private readonly IOutput output;
        public KeyboardInput(IOutput output)
        {
            this.output = output;
        }
        public string GetStringInput(string prompt)
        {
            while (true)
            {
                output.PrintPrompt(prompt);
                string userInput = Console.ReadLine() ?? "".Trim();
                if (ValidationHelper.IsValidStringInput(userInput))
                    return userInput;
            }
        }
        public int GetIntInput(string prompt)
        {
            while (true)
            {
                string userInputAsString = GetStringInput(prompt);
                if (ValidationHelper.IsValidIntInput(userInputAsString))
                    return Convert.ToInt32(userInputAsString);
            }
        }
    }
}
