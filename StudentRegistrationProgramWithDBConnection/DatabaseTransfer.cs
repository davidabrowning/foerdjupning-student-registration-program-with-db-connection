namespace StudentRegistrationProgramWithDBConnection
{
    internal class DatabaseTransfer: IDataTransfer
    {
        private readonly ProgramDbContext dbContext = new ProgramDbContext();
        public void Add(Student student)
        {
            dbContext.Add(student);
            dbContext.SaveChanges();
        }
        public void Update(Student original, Student updated)
        {
            original.FirstName = updated.FirstName;
            original.LastName = updated.LastName;
            original.City = updated.City;
            dbContext.SaveChanges();
        }
        public List<Student> AllStudents()
        {
            return dbContext.Students.ToList();
        }
        public bool IsValidStudentId(int studentId)
        {
            return dbContext.Students.Where(s => s.StudentId == studentId).Any();
        }
        public int StudentCount()
        {
            return AllStudents().Count();
        }
    }
}
