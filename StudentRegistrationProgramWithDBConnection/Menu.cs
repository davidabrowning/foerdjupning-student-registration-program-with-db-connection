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

        private const string MainMenuTitle = "Huvudmeny";
        private const string MainMenuOptionRegister = "Registrera ny student";
        private const string MainMenuOptionEditOne = "Ändra student";
        private const string MainMenuOptionListAll = "Lista alla studenter";
        private const string MainMenuOptionQuit = "Avsluta programmet";
        private const string MainMenuPrompt = "Ditt val:";
        private const string RegisterMenuTitle = "Registrera ny student";
        private const string EditMenuTitle = "Ändrar student";
        private const string EditMenuPromptStudentId = "Student att ändra (ange student id-nummer):";
        private const string ListAllMenuTitle = "Lista alla studenter";
        private const string QuitMenuTitle = "Avsluta programmet";
        private const string InvalidMenuInputTitle = "Oväntad inmatning";
        private const string PromptFirstName = "Förnamn:";
        private const string PromptLastName = "Efternamn:";
        private const string PromptCity = "Stad:";
        private const string SuccessStudentRegistered = "Ny student registerad.";
        private const string SuccessStudentEdited = "Student uppdaterad.";
        private const string SuccessListComplete = "Listan klar.";
        private const string SuccessGoodbye = "Tack och hej då!";
        private const string WarningStudentIdNotFound = "Lyckades inte hitta student med detta id-nummer.";
        private const string WarningUnexpectedInput = "Oväntad inmatning. Försök igen.";
        private const string WarningStudentIsNull = "Student är null.";

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
            databaseTransfer.Add(student);
            printer.PrintSuccess(SuccessStudentRegistered);
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
        public void ShowEditMenu()
        {
            printer.PrintTitle(EditMenuTitle);
            printer.PrintList<Student>(databaseTransfer.AllStudents());
            int idToEdit = keyboard.GetIntInput(EditMenuPromptStudentId);
            if (databaseTransfer.IsValidStudentId(idToEdit))
                EditStudent(idToEdit);
            else
                printer.PrintWarning(WarningStudentIdNotFound);
            printer.ConfirmToContinue();
            ShowMainMenu();
        }
        private void EditStudent(int studentId)
        {
            Student? originalStudent = databaseTransfer.AllStudents().Where(s => s.StudentId == studentId).FirstOrDefault();
            Student updatedStudentInfo = GetNewStudentFromUser();
            databaseTransfer.Update(originalStudent, updatedStudentInfo);
            printer.PrintSuccess(SuccessStudentEdited);
        }
        public void ShowStudentList()
        {
            printer.PrintTitle(ListAllMenuTitle);
            foreach (Student student in databaseTransfer.AllStudents())
               printer.PrintMessage(student.ToString() ?? WarningStudentIsNull);
            printer.PrintSuccess(SuccessListComplete);
            printer.ConfirmToContinue();
            ShowMainMenu();
        }
        public void ShowQuitProgram()
        {
            printer.PrintTitle(QuitMenuTitle);
            printer.PrintSuccess(SuccessGoodbye);
            printer.ConfirmToContinue();
            printer.Clear();
        }

        public void ShowInvalidMenuInput()
        {
            printer.PrintTitle(InvalidMenuInputTitle);
            printer.PrintWarning(WarningUnexpectedInput);
            printer.ConfirmToContinue();
            ShowMainMenu();
        }
    }
}
