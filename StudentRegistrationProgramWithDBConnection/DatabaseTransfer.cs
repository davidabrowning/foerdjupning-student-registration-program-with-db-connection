﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection
{
    internal class DatabaseTransfer
    {
        private ProgramDbContext dbContext = new ProgramDbContext();
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
        public DbSet<Student> AllStudents()
        {
            return dbContext.Students;
        }
    }
}
