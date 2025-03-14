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
        private const string MainMenuTitle = "Huvudmeny";
        private const string MainMenuOptionRegister = "Registrera ny student";
        private const string MainMenuOptionEditOne = "Ändra student";
        private const string MainMenuOptionListAll = "Lista alla studenter";
        private const string MainMenuOptionQuit = "Avsluta programmet";
        private const string MainMenuPrompt = "Ditt val:";
        private const string RegisterMenuTitle = "Registrera ny student";
        private const string EditMenuTitle = "Ändra student";
        private const string EditMenuPromptStudentId = "Student att ändra (ange student id-nummer):";
        private const string ListAllMenuTitle = "Lista alla studenter";
        private const string QuitMenuTitle = "Avsluta programmet";
        private const string PromptFirstName = "Förnamn:";
        private const string PromptLastName = "Efternamn:";
        private const string PromptCity = "Stad:";
        private const string SuccessStudentRegistered = "Ny student registerad.";
        private const string SuccessStudentEdited = "Student uppdaterad.";
        private const string SuccessGoodbye = "Tack och hej då!";
        private const string WarningStudentIdNotFound = "Lyckades inte hitta student med detta id-nummer.";
        private const string WarningUnexpectedInput = "Oväntad inmatning. Försök igen.";
        private const string WarningStudentIsNull = "Student är null.";

        private readonly IOutput output;
        private readonly IInput input;
        private readonly IDataTransfer dataTransfer;

        public Menu(IOutput output, IInput input, IDataTransfer databaseTransfer)
        {
            this.output = output;
            this.input = input;
            this.dataTransfer = databaseTransfer;
        }
        public void Go()
        {
            ShowMainMenu();
        }
        public void ShowMainMenu()
        {
            output.PrintTitle(MainMenuTitle);
            output.PrintSectionDivider();
            PrintMainMenuOptions();
            output.PrintSectionDivider();
            output.PrintPrompt(MainMenuPrompt);

            HandleMainMenuSelection();
        }
        private void PrintMainMenuOptions()
        {
            bool atLeastOneStudentIsRegistered = dataTransfer.StudentCount() > 0;
            output.PrintMessage($"[1] {MainMenuOptionRegister}");
            if (atLeastOneStudentIsRegistered)
            {
                output.PrintMessage($"[2] {MainMenuOptionEditOne}");
                output.PrintMessage($"[3] {MainMenuOptionListAll}");
            }
            else
            {
                output.PrintInactive($"[2] {MainMenuOptionEditOne}");
                output.PrintInactive($"[3] {MainMenuOptionListAll}");
            }
            output.PrintMessage($"[Q] {MainMenuOptionQuit}");
        }
        public void HandleMainMenuSelection()
        {
            bool atLeastOneStudentIsRegistered = dataTransfer.StudentCount() > 0;
            switch (input.GetStringInput().ToUpper())
            {
                case "1":
                    ShowRegistrationMenu();
                    break;
                case "2":
                    if (atLeastOneStudentIsRegistered)
                        ShowEditMenu();
                    else
                        ShowInvalidMenuInput();
                        break;
                case "3":
                    if (atLeastOneStudentIsRegistered)
                        ShowStudentList();
                    else
                        ShowInvalidMenuInput();
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
            output.PrintTitle(RegisterMenuTitle);
            output.PrintSectionDivider();
            Student student = RegisterStudent();
            output.PrintSectionDivider();
            PrintStudent(student);
            output.PrintSectionDivider();
            output.PrintSuccess(SuccessStudentRegistered);
            output.PrintSectionDivider();
            output.ConfirmToContinue();

            ShowMainMenu();
        }
        private Student RegisterStudent()
        {
            Student student = GetNewStudentFromUser();
            dataTransfer.Add(student);
            return student;
        }
        private void PrintStudent(Student student)
        {
            output.PrintMessage(student.ToString());
        }
        private Student GetNewStudentFromUser()
        {
            return new Student()
            {
                FirstName = input.GetStringInput(PromptFirstName),
                LastName = input.GetStringInput(PromptLastName),
                City = input.GetStringInput(PromptCity)
            };
        }
        public void ShowEditMenu()
        {
            output.PrintTitle(EditMenuTitle);
            output.PrintSectionDivider();
            output.PrintList<Student>(dataTransfer.AllStudents());
            output.PrintSectionDivider();
            int idToEdit = input.GetIntInput(EditMenuPromptStudentId);
            output.PrintSectionDivider();
            if (dataTransfer.IsValidStudentId(idToEdit))
                EditStudent(idToEdit);
            else
                output.PrintWarning(WarningStudentIdNotFound);
            output.PrintSectionDivider();
            output.ConfirmToContinue();

            ShowMainMenu();
        }
        private void EditStudent(int studentId)
        {
            output.PrintTitle(EditMenuTitle);
            output.PrintSectionDivider();
            Student? originalStudent = dataTransfer.AllStudents().Where(s => s.StudentId == studentId).FirstOrDefault();
            if (originalStudent == null)
            {
                output.PrintWarning(WarningStudentIdNotFound);
                return;
            }
            output.PrintMessage(originalStudent.ToString());
            output.PrintSectionDivider();
            Student updatedStudentInfo = GetNewStudentFromUser();
            output.PrintSectionDivider();
            dataTransfer.Update(originalStudent, updatedStudentInfo);
            output.PrintMessage(originalStudent.ToString());
            output.PrintSectionDivider();
            output.PrintSuccess(SuccessStudentEdited);
        }
        public void ShowStudentList()
        {
            output.PrintTitle(ListAllMenuTitle);
            output.PrintSectionDivider();
            foreach (Student student in dataTransfer.AllStudents())
               output.PrintMessage(student.ToString() ?? WarningStudentIsNull);
            output.PrintSectionDivider();
            output.ConfirmToContinue();

            ShowMainMenu();
        }
        public void ShowQuitProgram()
        {
            output.PrintTitle(QuitMenuTitle);
            output.PrintSectionDivider();
            output.PrintMessage(SuccessGoodbye);
            output.PrintSectionDivider();
            output.ConfirmToContinue();

            output.Clear();
        }

        public void ShowInvalidMenuInput()
        {
            output.PrintSectionDivider();
            output.PrintWarning(WarningUnexpectedInput);
            output.PrintSectionDivider();
            output.ConfirmToContinue();

            ShowMainMenu();
        }
    }
}
