using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection
{
    internal class ProgramLauncher
    {
        public void Go()
        {
            Printer printer = new Printer();
            Keyboard keyboard = new Keyboard(printer);
            DatabaseTransfer databaseTransfer = new DatabaseTransfer();
            Menu menu = new Menu(printer, keyboard, databaseTransfer);
            menu.Go();
        }
    }
}
