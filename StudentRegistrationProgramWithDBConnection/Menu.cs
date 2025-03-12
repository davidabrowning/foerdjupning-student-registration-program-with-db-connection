using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection
{
    internal class Menu
    {
        public void Go()
        {
            ShowMainMenu();
        }
        public void ShowMainMenu()
        {
            Console.WriteLine("Huvudmeny");
            Console.WriteLine("[1] Registrera ny student");
            Console.WriteLine("[2] Ändra student");
            Console.WriteLine("[3] Lista alla studenter");
            Console.WriteLine("[Q] Avsluta programmet");
            HandleMainMenuSelection();
        }
        public void HandleMainMenuSelection()
        {
            switch(Console.ReadLine().Trim().ToUpper())
            {
                case "1":
                    ShowRegistrationMenu();
                    break;
                case "2":
                    ShowEditMenu();
                    break;
                case "3":
                    ListAllStudents();
                    break;
                case "Q":
                    QuitProgram();
                    break;
                default:
                    Console.WriteLine("Oväntad inmatning. Tryck ENTER för att återgå till huvudmenyn.");
                    Console.ReadLine();
                    ShowMainMenu();
                    break;
            }
        }
        public void ShowRegistrationMenu()
        {
            Console.WriteLine("Registerar ny student...");
            Console.WriteLine("Tryck ENTER för att återgå till huvudmenyn.");
            Console.ReadLine();
            ShowMainMenu();
        }
        public void ShowEditMenu()
        {
            Console.WriteLine("Ändrar existerande student");
            Console.WriteLine("Tryck ENTER för att återgå till huvudmenyn.");
            Console.ReadLine();
            ShowMainMenu();
        }
        public void ListAllStudents()
        {
            Console.WriteLine("Listar alla studenter");
            Console.WriteLine("Tryck ENTER för att återgå till huvudmenyn.");
            Console.ReadLine();
            ShowMainMenu();
        }
        public void QuitProgram()
        {
            Console.WriteLine("Tack och hej då!");
        }
    }
}
