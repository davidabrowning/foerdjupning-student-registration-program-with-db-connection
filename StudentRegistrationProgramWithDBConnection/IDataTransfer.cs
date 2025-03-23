namespace StudentRegistrationProgramWithDBConnection
{
    internal interface IDataTransfer
    {
        void Add(Student student);
        void Update(Student originalStudent, Student updatedStudent);
        bool IsValidStudentId(int studentId);
        int StudentCount();
        List<Student> AllStudents();
    }
}
