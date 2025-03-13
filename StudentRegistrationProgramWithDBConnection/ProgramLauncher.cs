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
            private Printer printer = new Printer();
            private Keyboard keyboard = new Keyboard(printer);
            private ProgramDbContext dbContext = new ProgramDbContext();
            Menu menu = new Menu(keyboard, printer, dbContext);
            menu.Go();
        }
    }
}
