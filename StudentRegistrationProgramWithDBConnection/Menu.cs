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
        private Printer printer = new Printer();
        private ProgramDbContext dbContext
            = new ProgramDbContext();
        public void Go()
        {
            ShowMainMenu();
        }
        public void ShowMainMenu()
        {
            printer.PrintTitle("Huvudmeny)");
            printer.PrintMessage("[1] Registrera ny student");
            printer.PrintMessage("[2] Ändra student");
            printer.PrintMessage("[3] Lista alla studenter");
            printer.PrintMessage("[Q] Avsluta programmet");
            printer.PrintPrompt("Ditt val: ");
            HandleMainMenuSelection();
        }
        public void HandleMainMenuSelection()
        {
            switch((Console.ReadLine() ?? "").Trim().ToUpper())
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
            printer.PrintTitle("Registerar ny student...");
            Student student = GetNewStudentFromUser();
            AddToDatabase(student);
            printer.ConfirmToContinue();
            ShowMainMenu();
        }
        private Student GetNewStudentFromUser()
        {
            printer.PrintPrompt("Förnamn: ");
            string firstName = Console.ReadLine();
            printer.PrintPrompt("Efternamn: ");
            string lastName = Console.ReadLine();
            printer.PrintPrompt("Stad: ");
            string city = Console.ReadLine();
            return new Student()
            {
                FirstName = firstName,
                LastName = lastName,
                City = city
            };
        }
        private void AddToDatabase(Student student)
        {
            dbContext.Add(student);
            dbContext.SaveChanges();
        }
        public void ShowEditMenu()
        {
            printer.PrintTitle("Ändrar existerande student");

            foreach (Student student in dbContext.Students)
                printer.PrintMessage(student.ToString());

            printer.PrintPrompt("Student att ändra (ange student id-nummer): ");
            if (int.TryParse(Console.ReadLine(), out int studentId))
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
            printer.PrintTitle("Listar alla studenter");
            foreach (Student student in dbContext.Students)
               printer.PrintMessage(student.ToString());
            printer.ConfirmToContinue();
            ShowMainMenu();
        }
        public void ShowQuitProgram()
        {
            printer.PrintTitle("Avsluta programmet");
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
