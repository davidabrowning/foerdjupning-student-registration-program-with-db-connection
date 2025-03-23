namespace StudentRegistrationProgramWithDBConnection.Interfaces
{
    internal interface IInput
    {
        string GetStringInput(string prompt);
        int GetIntInput(string prompt);
    }
}
