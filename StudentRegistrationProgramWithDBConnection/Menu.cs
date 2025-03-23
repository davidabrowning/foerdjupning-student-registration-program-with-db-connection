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
            output.PrintSectionDivider();
            PrintMainMenuOptions();
            output.PrintSectionDivider();
            output.PrintPrompt(MenuHelper.MainMenuPrompt);

            HandleMainMenuSelection();
        }

        private void PrintMainMenuOptions()
        {
            bool atLeastOneStudentIsRegistered = dataTransfer.StudentCount() > 0;
            output.PrintMessage($"[1] {MenuHelper.MainMenuOptionRegister}");
            if (atLeastOneStudentIsRegistered)
            {
                // Menu options styled to look normal
                output.PrintMessage($"[2] {MenuHelper.MainMenuOptionEditOne}");
                output.PrintMessage($"[3] {MenuHelper.MainMenuOptionListAll}");
            }
            else
            {
                // Menu options styled to look inactive
                output.PrintInactive($"[2] {MenuHelper.MainMenuOptionEditOne}");
                output.PrintInactive($"[3] {MenuHelper.MainMenuOptionListAll}");
            }
            output.PrintMessage($"[Q] {MenuHelper.MainMenuOptionQuit}");
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
            output.PrintTitle(MenuHelper.RegisterMenuTitle);
            output.PrintSectionDivider();
            Student student = RegisterStudent();
            output.PrintSectionDivider();
            PrintStudent(student);
            output.PrintSectionDivider();
            output.PrintSuccess(MenuHelper.SuccessStudentRegistered);
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
            output.PrintMessage(student.ToString() ?? MenuHelper.WarningStudentIsNull);
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
            output.PrintSectionDivider();
            output.PrintList<Student>(dataTransfer.AllStudents());
            output.PrintSectionDivider();
            int idToEdit = input.GetIntInput(MenuHelper.EditMenuPromptStudentId);
            output.PrintSectionDivider();
            if (dataTransfer.IsValidStudentId(idToEdit))
                EditStudent(idToEdit);
            else
                output.PrintWarning(MenuHelper.WarningStudentIdNotFound);
            output.PrintSectionDivider();
            output.ConfirmToContinue();

            ShowMainMenu();
        }

        private void EditStudent(int studentId)
        {
            output.PrintTitle(MenuHelper.EditMenuTitle);
            output.PrintSectionDivider();
            Student? originalStudent = dataTransfer.AllStudents().Where(s => s.StudentId == studentId).FirstOrDefault();
            if (originalStudent == null)
            {
                output.PrintWarning(MenuHelper.WarningStudentIdNotFound);
                return;
            }
            output.PrintMessage(originalStudent.ToString() ?? MenuHelper.WarningStudentIsNull);
            output.PrintSectionDivider();
            Student updatedStudentInfo = GetNewStudentFromUser();
            output.PrintSectionDivider();
            dataTransfer.Update(originalStudent, updatedStudentInfo);
            output.PrintMessage(originalStudent.ToString() ?? MenuHelper.WarningStudentIsNull);
            output.PrintSectionDivider();
            output.PrintSuccess(MenuHelper.SuccessStudentEdited);
        }

        public void ShowStudentList()
        {
            output.PrintTitle(MenuHelper.ListAllMenuTitle);
            output.PrintSectionDivider();
            foreach (Student student in dataTransfer.AllStudents())
               output.PrintMessage(student.ToString() ?? MenuHelper.WarningStudentIsNull);
            output.PrintSectionDivider();
            output.ConfirmToContinue();

            ShowMainMenu();
        }

        public void ShowQuitProgram()
        {
            output.PrintTitle(MenuHelper.QuitMenuTitle);
            output.PrintSectionDivider();
            output.PrintMessage(MenuHelper.SuccessGoodbye);
            output.PrintSectionDivider();
            output.ConfirmToContinue();

            output.Clear();
        }

        public void ShowInvalidMenuInput()
        {
            output.PrintSectionDivider();
            output.PrintWarning(MenuHelper.WarningUnexpectedInput);
            output.PrintSectionDivider();
            output.ConfirmToContinue();

            ShowMainMenu();
        }
    }
}
