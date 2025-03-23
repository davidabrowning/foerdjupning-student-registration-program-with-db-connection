namespace StudentRegistrationProgramWithDBConnection
{
    internal class ProgramLauncher
    {
        public void Go()
        {
            IOutput output = new Printer();
            IInput input = new Keyboard(output);
            IRepository dataTransfer = new DatabaseTransfer();
            Menu menu = new Menu(output, input, dataTransfer);
            menu.Go();
        }
    }
}
