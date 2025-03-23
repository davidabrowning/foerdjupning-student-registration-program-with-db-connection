namespace StudentRegistrationProgramWithDBConnection
{
    internal class ProgramLauncher
    {
        public void Go()
        {
            IOutput output = new ConsoleOutput();
            IInput input = new KeyboardInput(output);
            IRepository repository = new DatabaseRepository();
            Menu menu = new Menu(output, input, repository);
            menu.Go();
        }
    }
}
