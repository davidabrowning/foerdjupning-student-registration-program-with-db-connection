namespace StudentRegistrationProgramWithDBConnection
{
    internal class Menu
    {

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
            output.PrintTitle(MenuHelper.MainMenuTitle);
            PrintMainMenuOptions();
            output.PrintPrompt(MenuHelper.MainMenuPrompt);

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
            return dataTransfer.StudentCount() > 0;
        }

        public void HandleMainMenuSelection()
        {
            switch (input.GetStringInput().ToUpper())
            {
                case "1":
                    ShowRegistrationMenu();
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
            dataTransfer.Add(student);
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
            output.PrintList<Student>(dataTransfer.AllStudents());
            int idToEdit = input.GetIntInput(MenuHelper.EditMenuPromptStudentId);
            output.PrintSectionDivider();
            if (dataTransfer.IsValidStudentId(idToEdit))
                EditStudent(idToEdit);
            else
                output.PrintWarning(MenuHelper.WarningStudentIdNotFound);
            output.ConfirmToContinue();

            ShowMainMenu();
        }

        private void EditStudent(int studentId)
        {
            output.PrintTitle(MenuHelper.EditMenuTitle);
            Student? originalStudent = dataTransfer.AllStudents().Where(s => s.StudentId == studentId).FirstOrDefault();
            if (originalStudent == null)
            {
                output.PrintWarning(MenuHelper.WarningStudentIdNotFound);
                return;
            }
            output.PrintNeutral(originalStudent.ToString() ?? MenuHelper.WarningStudentIsNull);
            Student updatedStudentInfo = GetNewStudentFromUser();
            output.PrintSectionDivider();
            dataTransfer.Update(originalStudent, updatedStudentInfo);
            output.PrintNeutral(originalStudent.ToString() ?? MenuHelper.WarningStudentIsNull);
            output.PrintSuccess(MenuHelper.SuccessStudentEdited);
        }

        public void ShowStudentList()
        {
            output.PrintTitle(MenuHelper.ListAllMenuTitle);
            output.PrintList<Student>(dataTransfer.AllStudents());
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
    }
}
