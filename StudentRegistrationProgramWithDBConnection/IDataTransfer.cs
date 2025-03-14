using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection
{
    internal interface IDataTransfer
    {
        void Add(Student student);
        List<Student> AllStudents();
        bool IsValidStudentId(int studentId);
        void Update(Student originalStudent, Student updatedStudent);
    }
}
