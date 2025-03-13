using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection
{
    internal class Printer
    {
        private ConsoleColor defaultColor = ConsoleColor.White;
        private void Clear() => Console.Clear();
        private void Print(string text, ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
            Console.Write($"     {text}");
            Console.ForegroundColor = defaultColor;
        }
        private void Print(string text) => Print(text, defaultColor);
        private void PrintLine(string text, ConsoleColor textColor) => Print($"{text}\n", textColor);
        private void PrintLine(string text) => PrintLine(text, defaultColor);
        public void PrintMessage(string text) => PrintLine(text);
        public void PrintSuccess(string text) => PrintLine($"Succé: {text}: ", ConsoleColor.Green);
        public void PrintWarning(string text) => PrintLine($"Varning: {text}: ", ConsoleColor.Yellow);
        public void PrintError(string text) => PrintLine($"Fel: {text}: ", ConsoleColor.Red);
        public void PrintInactive(string text) => PrintLine(text, ConsoleColor.Gray);
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
        }
        public void ConfirmToContinue()
        {
            PrintInactive("Tryck ENTER för att fortsätta.");
            Console.ReadLine();
        }
    }
}
