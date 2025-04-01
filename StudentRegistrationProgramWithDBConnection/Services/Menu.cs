using Part2RegistrationProgramWithDB.Services;
using StudentRegistrationProgramWithDBConnection.Interfaces;
using StudentRegistrationProgramWithDBConnection.Models;
using StudentRegistrationProgramWithDBConnection.Utilities;

namespace StudentRegistrationProgramWithDBConnection.Services
{
    internal class Menu
    {

        private readonly IOutput output;
        private readonly IInput input;
        private readonly IRepository repository;

        public Menu(IOutput output, IInput input, IRepository repository)
        {
            this.output = output;
            this.input = input;
            this.repository = repository;
        }

        public void Go()
        {
            ShowLoginMenu();
        }

        public void ShowLoginMenu()
        {
            output.PrintTitle(MenuHelper.LoginMenuTitle);
            string username = input.GetStringInput(MenuHelper.PromptUsername);
            string password = input.GetStringInput(MenuHelper.PromptPassword);
            if (repository.IsValidUsernameAndPassword(username, password))
            {
                UserSession.SystemUser = repository.GetSystemUser(username);
                ShowMainMenu();
            }
            else
            {
                output.PrintWarning(MenuHelper.WarningInvalidUsernameOrPassword);
                output.ConfirmToContinue();
                ShowLoginMenu();
            }
        }

        public void ShowMainMenu()
        {
            output.PrintTitle(MenuHelper.MainMenuTitle);
            PrintMainMenuOptions();

            HandleMainMenuSelection();
        }

        private void PrintMainMenuOptions()
        {
            if (AtLeastOneStudentIsRegistered())
            {
                // All options are styled to look active
                output.PrintListItemActive($"[1] {MenuHelper.MainMenuOptionRegister}");
                output.PrintListItemActive($"[2] {MenuHelper.MainMenuOptionEditOne}");
                output.PrintListItemActive($"[3] {MenuHelper.MainMenuOptionListAll}");
                output.PrintListItemActive($"[Q] {MenuHelper.MainMenuOptionQuit}");
            }
            else
            {
                // Options 2-3 are styled to look inactive
                output.PrintListItemActive($"[1] {MenuHelper.MainMenuOptionRegister}");
                output.PrintListItemInactive($"[2] {MenuHelper.MainMenuOptionEditOne}");
                output.PrintListItemInactive($"[3] {MenuHelper.MainMenuOptionListAll}");
                output.PrintListItemActive($"[Q] {MenuHelper.MainMenuOptionQuit}");
            }
            output.PrintSectionDivider();
        }

        private bool AtLeastOneStudentIsRegistered()
        {
            return repository.StudentCount() > 0;
        }

        public void HandleMainMenuSelection()
        {
            switch (input.GetStringInput(MenuHelper.MainMenuPrompt).ToUpper())
            {
                case "1":
                    if (UserSession.SystemUser.UserRole.CanAddStudent)
                        ShowRegistrationMenu();
                    else
                        ShowAccessRestricted();
                    break;
                case "2":
                    if (AtLeastOneStudentIsRegistered())
                        ShowEditMenu();
                    else
                        ShowInvalidMenuInput();
                        break;
                case "3":
                    if (AtLeastOneStudentIsRegistered())
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
            output.PrintTitle(MenuHelper.RegisterMenuTitle);
            Student student = RegisterStudent();
            PrintStudent(student);
            output.PrintSuccess(MenuHelper.SuccessStudentRegistered);
            output.ConfirmToContinue();

            ShowMainMenu();
        }

        private Student RegisterStudent()
        {
            Student student = GetNewStudentFromUser();
            repository.Add(student);
            output.PrintSectionDivider();
            return student;
        }

        private void PrintStudent(Student student)
        {
            output.PrintNeutral(student.ToString() ?? MenuHelper.WarningStudentIsNull);
        }

        private Student GetNewStudentFromUser()
        {
            return new Student()
            {
                FirstName = input.GetStringInput(MenuHelper.PromptFirstName),
                LastName = input.GetStringInput(MenuHelper.PromptLastName),
                City = input.GetStringInput(MenuHelper.PromptCity)
            };
        }

        public void ShowEditMenu()
        {
            output.PrintTitle(MenuHelper.EditMenuTitle);
            output.PrintList(repository.AllStudents());
            int idToEdit = input.GetIntInput(MenuHelper.EditMenuPromptStudentId);
            output.PrintSectionDivider();
            if (repository.IsValidStudentId(idToEdit))
                EditStudent(idToEdit);
            else
                output.PrintWarning(MenuHelper.WarningStudentIdNotFound);
            output.ConfirmToContinue();

            ShowMainMenu();
        }

        private void EditStudent(int studentId)
        {
            output.PrintTitle(MenuHelper.EditMenuTitle);
            Student? originalStudent = repository.AllStudents().Where(s => s.StudentId == studentId).FirstOrDefault();
            if (originalStudent == null)
            {
                output.PrintWarning(MenuHelper.WarningStudentIdNotFound);
                return;
            }
            output.PrintNeutral(originalStudent.ToString() ?? MenuHelper.WarningStudentIsNull);
            Student updatedStudentInfo = GetNewStudentFromUser();
            output.PrintSectionDivider();
            repository.Update(originalStudent, updatedStudentInfo);
            output.PrintNeutral(originalStudent.ToString() ?? MenuHelper.WarningStudentIsNull);
            output.PrintSuccess(MenuHelper.SuccessStudentEdited);
        }

        public void ShowStudentList()
        {
            output.PrintTitle(MenuHelper.ListAllMenuTitle);
            output.PrintList(repository.AllStudents());
            output.ConfirmToContinue();

            ShowMainMenu();
        }

        public void ShowQuitProgram()
        {
            output.PrintTitle(MenuHelper.QuitMenuTitle);
            output.PrintNeutral(MenuHelper.SuccessGoodbye);
            output.ConfirmToContinue();

            output.Clear();
        }

        public void ShowInvalidMenuInput()
        {
            output.PrintSectionDivider();
            output.PrintWarning(MenuHelper.WarningUnexpectedInput);
            output.ConfirmToContinue();

            ShowMainMenu();
        }

        public void ShowAccessRestricted()
        {
            output.PrintSectionDivider();
            output.PrintWarning(MenuHelper.WarningAccessRestricted);
            output.ConfirmToContinue();

            ShowMainMenu();
        }
    }
}
