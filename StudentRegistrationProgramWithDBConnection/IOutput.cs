namespace StudentRegistrationProgramWithDBConnection
{
    internal interface IOutput
    {
        void PrintTitle(string text);
        void PrintNeutral(string text);
        void PrintError(string text);
        void PrintWarning(string text);
        void PrintSuccess(string text);
        void PrintInactive(string text);
        void PrintPrompt(string text);
        void PrintList<T>(IEnumerable<T> list);
        void PrintSectionDivider();
        void ConfirmToContinue();
        void PrintLine();
        void Clear();
    }
}
