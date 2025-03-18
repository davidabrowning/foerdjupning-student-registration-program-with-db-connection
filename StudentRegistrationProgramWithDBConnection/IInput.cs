namespace StudentRegistrationProgramWithDBConnection
{
    internal interface IInput
    {
        string GetStringInput();
        string GetStringInput(string prompt);
        int GetIntInput();
        int GetIntInput(string prompt);
    }
}
