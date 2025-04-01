using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Part2RegistrationProgramWithDB.Models;
using StudentRegistrationProgramWithDBConnection.Models;

namespace StudentRegistrationProgramWithDBConnection.DTOs
{
    internal class ProgramDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(new ConfigurationBuilder()
                .AddJsonFile("Configurations/appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["Part2ApplicationDb"]);
        }
    }
}
