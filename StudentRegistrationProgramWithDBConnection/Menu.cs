using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
        private DatabaseTransfer databaseTransfer;
        public Menu(Printer printer, Keyboard keyboard, DatabaseTransfer databaseTransfer)
        {
            this.printer = printer;
            this.keyboard = keyboard;
            this.databaseTransfer = databaseTransfer;
        }
        public void Go()
        {
            ShowMainMenu();
        }
        public void ShowMainMenu()
        {
            printer.PrintTitle("Huvudmeny");
            printer.PrintMessage("[1] Registrera ny student");
            printer.PrintMessage("[2] Ändra student");
            printer.PrintMessage("[3] Lista alla studenter");
            printer.PrintMessage("[Q] Avsluta programmet");
            printer.PrintPrompt("Ditt val: ");
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
            printer.PrintTitle("Registerar ny student...");
            Student student = GetNewStudentFromUser();
            databaseTransfer.Add(student);
            printer.PrintSuccess("Ny student registerad.");
            printer.ConfirmToContinue();
            ShowMainMenu();
        }
        private Student GetNewStudentFromUser()
        {
            return new Student()
            {
                FirstName = keyboard.GetStringInput("Förnamn: "),
                LastName = keyboard.GetStringInput("Efternamn: "),
                City = keyboard.GetStringInput("Stad: ")
            };
        }
        public void ShowEditMenu()
        {
            printer.PrintTitle("Ändrar existerande student");
            printer.PrintList<Student>(databaseTransfer.AllStudents());
            int idToEdit = keyboard.GetIntInput("Student att ändra (ange student id-nummer): ");
            if (databaseTransfer.IsValidStudentId(idToEdit))
                EditStudent(idToEdit);
            else
                printer.PrintWarning("Lyckades inte hitta student med detta id-nummer.");
            printer.ConfirmToContinue();
            ShowMainMenu();
        }
        private void EditStudent(int studentId)
        {
            Student originalStudent = databaseTransfer.AllStudents().Where(s => s.StudentId == studentId).FirstOrDefault();
            Student updatedStudentInfo = GetNewStudentFromUser();
            databaseTransfer.Update(originalStudent, updatedStudentInfo);
            printer.PrintSuccess("Student uppdaterad.");
        }
        public void ShowStudentList()
        {
            printer.PrintTitle("Listar alla studenter");
            foreach (Student student in databaseTransfer.AllStudents())
               printer.PrintMessage(student.ToString());
            printer.PrintSuccess("Listan klar.");
            printer.ConfirmToContinue();
            ShowMainMenu();
        }
        public void ShowQuitProgram()
        {
            printer.PrintTitle("Avsluta programmet");
            printer.PrintMessage("Tack och hej då!");
            printer.ConfirmToContinue();
            printer.Clear();
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
