using StudentRegistrationProgramWithDBConnection.Services;

namespace StudentRegistrationProgramWithDBConnection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProgramLauncher programLauncher = new ProgramLauncher();
            programLauncher.Go();
        }
    }
}
