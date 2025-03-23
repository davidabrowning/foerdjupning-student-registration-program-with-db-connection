using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentRegistrationProgramWithDBConnection.Models;

namespace StudentRegistrationProgramWithDBConnection
{
    internal class ProgramDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(new ConfigurationBuilder()
                .AddJsonFile("Configurations/appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["ApplicationDb"]);
        }
    }
}
