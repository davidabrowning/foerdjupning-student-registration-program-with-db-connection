namespace StudentRegistrationProgramWithDBConnection
{
    internal class Keyboard : IInput
    {
        private readonly IOutput output;
        public Keyboard(IOutput output)
        {
            this.output = output;
        }
        public string GetStringInput()
        {
            return (Console.ReadLine() ?? "").Trim();
        }
        public string GetStringInput(string prompt)
        {
            output.PrintPrompt(prompt);
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
            output.PrintPrompt(prompt);
            return GetIntInput();
        }
    }
}
