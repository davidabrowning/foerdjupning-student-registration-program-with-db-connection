namespace StudentRegistrationProgramWithDBConnection.Models
{
    internal class Student
    {
        public int StudentId { get; set; } = 0;
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string City { get; set; } = "";

        public override string? ToString()
        {
            return $"{FirstName} {LastName} ({City}) [id: {StudentId}]";
        }
    }
}
