using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection
{
    internal class Menu
    {
        private StudentRegistrationProgramWithDBConnectionDBContext dbContext = new StudentRegistrationProgramWithDBConnectionDBContext();
        public void Go()
        {
            ShowMainMenu();
        }
        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Huvudmeny");
            Console.WriteLine("[1] Registrera ny student");
            Console.WriteLine("[2] Ändra student");
            Console.WriteLine("[3] Lista alla studenter");
            Console.WriteLine("[Q] Avsluta programmet");
            HandleMainMenuSelection();
        }
        public void HandleMainMenuSelection()
        {
            switch((Console.ReadLine() ?? "").Trim().ToUpper())
            {
                case "1":
                    ShowRegistrationMenu();
                    break;
                case "2":
                    ShowEditMenu();
                    break;
                case "3":
                    ListAllStudents();
                    break;
                case "Q":
                    QuitProgram();
                    break;
                default:
                    Console.WriteLine("Oväntad inmatning.");
                    ConfirmToContinue();
                    ShowMainMenu();
                    break;
            }
        }
        public void ShowRegistrationMenu()
        {
            Console.Clear();
            Console.WriteLine("Registerar ny student...");

            // Get student info
            Console.Write("Förnamn: ");
            string firstName = Console.ReadLine();
            Console.Write("Efternamn: ");
            string lastName = Console.ReadLine();
            Console.Write("Stad: ");
            string city = Console.ReadLine();
            Student student = new Student()
            {
                FirstName = firstName,
                LastName = lastName,
                City = city
            };

            // Add student to database
            dbContext.Add(student);
            dbContext.SaveChanges();

            ConfirmToContinue();
            ShowMainMenu();
        }
        public void ShowEditMenu()
        {
            Console.Clear();
            Console.WriteLine("Ändrar existerande student");

            foreach (Student student in dbContext.Students)
            {
                Console.WriteLine(student);
            }

            Console.Write("Student att ändra (ange student id-nummer): ");
            if (int.TryParse(Console.ReadLine(), out int studentId))
            {
                Student student = dbContext.Students.Where(s => s.StudentId == studentId).FirstOrDefault();
                if (student != null)
                {
                    Console.Write("Förnamn: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Efternamn: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Stad: ");
                    string city = Console.ReadLine();
                    student.FirstName = firstName;
                    student.LastName = lastName;
                    student.City = city;
                    dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Lyckades inte hitta student med detta id-nummer.");
                }
            }
            else
            {
                Console.WriteLine("Lyckades inte hitta student med detta id-nummer.");
            }

            ConfirmToContinue();
            ShowMainMenu();
        }
        public void ListAllStudents()
        {
            Console.Clear();
            Console.WriteLine("Listar alla studenter");

            foreach (Student student in dbContext.Students)
            {
                Console.WriteLine(student);
            }

            ConfirmToContinue();
            ShowMainMenu();
        }
        public void QuitProgram()
        {
            Console.Clear();
            Console.WriteLine("Tack och hej då!");
        }
        public void ConfirmToContinue()
        {
            Console.WriteLine("Tryck ENTER för att fortsätta.");
            Console.ReadLine();
        }
    }
}
