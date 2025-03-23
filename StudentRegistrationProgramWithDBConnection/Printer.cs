namespace StudentRegistrationProgramWithDBConnection
{
    internal class Printer : IOutput
    {
        private const ConsoleColor defaultColor = ConsoleColor.White;

        public void Clear() => Console.Clear();
        private void Indent() => Console.Write("     ");
        private void Print(string text, ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
            Indent();
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }
        private void Print(string text) => Print(text, defaultColor);
        private void PrintLine(string text, ConsoleColor textColor) => Print($"{text}\n", textColor);
        private void PrintLine(string text) => PrintLine(text, defaultColor);
        public void PrintLine() => PrintLine("");
        public void PrintSectionDivider() => PrintLine();
        private void PrintSection(string text, ConsoleColor textColor)
        {
            PrintLine(text, defaultColor);
            PrintSectionDivider();
        }
        public void PrintMessage(string text) => PrintLine(text);
        public void PrintInactive(string text) => PrintLine(text, ConsoleColor.DarkGray);
        public void PrintSuccess(string text) => PrintSection(text, ConsoleColor.Green);
        public void PrintWarning(string text) => PrintSection($"Varning: {text}", ConsoleColor.Yellow);
        public void PrintError(string text) => PrintSection($"Fel: {text}", ConsoleColor.Red);
        public void PrintPrompt(string text)
        {
            Print($"{text} ", ConsoleColor.Cyan);
            Console.ForegroundColor= ConsoleColor.Cyan;
        }
        public void PrintTitle(string text)
        {
            Clear();
            PrintLine("\n");
            PrintLine($"===== {text} =====");
            PrintSectionDivider();
        }
        public void ConfirmToContinue()
        {
            PrintInactive("Tryck ENTER för att fortsätta.");
            Indent();
            Console.ReadLine();
        }
        public void PrintList<T>(IEnumerable<T> tList)
        {
            foreach (T t in tList)
                if (t != null)
                    PrintMessage(t.ToString() ?? "");
        }
    }
}
