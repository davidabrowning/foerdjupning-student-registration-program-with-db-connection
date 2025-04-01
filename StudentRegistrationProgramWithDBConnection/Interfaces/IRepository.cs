using Part2RegistrationProgramWithDB.Models;
using StudentRegistrationProgramWithDBConnection.Models;

namespace StudentRegistrationProgramWithDBConnection.Interfaces
{
    internal interface IRepository
    {
        void Add(Student student);
        List<Student> AllStudents();
        bool IsValidStudentId(int studentId);
        void Update(Student originalStudent, Student updatedStudent);
        int StudentCount();
        bool IsValidUsernameAndPassword(string username, string password);
        SystemUser? GetSystemUser(string username);
    }
}
