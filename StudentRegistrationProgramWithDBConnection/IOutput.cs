namespace StudentRegistrationProgramWithDBConnection
{
    internal interface IOutput
    {
        void PrintTitle(string text);
        void PrintNeutral(string text);
        void PrintError(string text);
        void PrintWarning(string text);
        void PrintSuccess(string text);
        void PrintListItemActive(string text);
        void PrintListItemInactive(string text);
        void PrintPrompt(string text);
        void PrintList<T>(IEnumerable<T> list);
        void PrintSectionDivider();
        void ConfirmToContinue();
        void PrintLine();
        void Clear();
    }
}
