using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection
{
    internal class Menu
    {
        private Printer printer;
        private Keyboard keyboard;
        private ProgramDbContext dbContext;

        private const string MainMenuTitle = "Huvudmeny";
        private const string MainMenuOptionRegister = "Registrera ny student";
        private const string MainMenuOptionEditOne = "Ändra student";
        private const string MainMenuOptionListAll = "Lista alla studenter";
        private const string MainMenuOptionQuit = "Avsluta programmet";
        private const string MainMenuPrompt = "Ditt val:";
        private const string RegisterMenuTitle = "Registrera ny student";
        private const string EditMenuTitle = "Ändra existerande student";
        private const string EditMenuPromptStudentId = "Student att ändra (ange student id-nummer):";
        private const string ListAllMenuTitle = "Lista alla studenter";
        private const string QuitMenuTitle = "Avsluta programmet";
        private const string PromptFirstName = "Förnamn:";
        private const string PromptLastName = "Efternamn:";
        private const string PromptCity = "Stad:";

        public Menu(Printer printer, Keyboard keyboard, ProgramDbContext dbContext)
        {
            this.printer = printer;
            this.keyboard = keyboard;
            this.dbContext = dbContext;
        }
        public void Go()
        {
            ShowMainMenu();
        }
        public void ShowMainMenu()
        {
            printer.PrintTitle(MainMenuTitle);
            printer.PrintMessage($"[1] {MainMenuOptionRegister}");
            printer.PrintMessage($"[2] {MainMenuOptionEditOne}");
            printer.PrintMessage($"[3] {MainMenuOptionListAll}");
            printer.PrintMessage($"[Q] {MainMenuOptionQuit}");
            printer.PrintPrompt(MainMenuPrompt);
            HandleMainMenuSelection();
        }
        public void HandleMainMenuSelection()
        {
            switch(keyboard.GetStringInput().ToUpper())
            {
                case "1":
                    ShowRegistrationMenu();
                    break;
                case "2":
                    ShowEditMenu();
                    break;
                case "3":
                    ShowStudentList();
                    break;
                case "Q":
                    ShowQuitProgram();
                    break;
                default:
                    ShowInvalidMenuInput();
                    break;
            }
        }
        public void ShowRegistrationMenu()
        {
            printer.PrintTitle(RegisterMenuTitle);
            Student student = GetNewStudentFromUser();
            AddToDatabase(student);
            printer.ConfirmToContinue();
            ShowMainMenu();
        }
        private Student GetNewStudentFromUser()
        {
            return new Student()
            {
                FirstName = keyboard.GetStringInput(PromptFirstName),
                LastName = keyboard.GetStringInput(PromptLastName),
                City = keyboard.GetStringInput(PromptCity)
            };
        }
        private void AddToDatabase(Student student)
        {
            dbContext.Add(student);
            dbContext.SaveChanges();
        }
        public void ShowEditMenu()
        {
            printer.PrintTitle(EditMenuTitle);

            foreach (Student student in dbContext.Students)
                printer.PrintMessage(student.ToString());

            if (int.TryParse(keyboard.GetStringInput(EditMenuPromptStudentId), out int studentId))
            {
                Student student = dbContext.Students.Where(s => s.StudentId == studentId).FirstOrDefault();
                if (student != null)
                {
                    Student updatedStudentInfo = GetNewStudentFromUser();
                    student.FirstName = updatedStudentInfo.FirstName;
                    student.LastName = updatedStudentInfo.LastName;
                    student.City = updatedStudentInfo.City;
                    dbContext.SaveChanges();
                }
                else
                {
                    printer.PrintWarning("Lyckades inte hitta student med detta id-nummer.");
                }
            }
            else
            {
                printer.PrintWarning("Lyckades inte hitta student med detta id-nummer.");
            }

            printer.ConfirmToContinue();
            ShowMainMenu();
        }
        public void ShowStudentList()
        {
            printer.PrintTitle(ListAllMenuTitle);
            foreach (Student student in dbContext.Students)
               printer.PrintMessage(student.ToString());
            printer.ConfirmToContinue();
            ShowMainMenu();
        }
        public void ShowQuitProgram()
        {
            printer.PrintTitle(QuitMenuTitle);
            printer.PrintMessage("Tack och hej då!");
            printer.ConfirmToContinue();
        }

        public void ShowInvalidMenuInput()
        {
            printer.PrintTitle("Oväntad inmatning");
            printer.PrintWarning("Oväntad inmatning. Försök igen.");
            printer.ConfirmToContinue();
            ShowMainMenu();
        }
    }
}
