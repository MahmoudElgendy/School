using School.Api.Models;
using System;

namespace School.Api.Data
{
    public static class StudentSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (context.Students.Any())
                return; // idempotent (runs only once)

            var students = new List<Student>
        {
            new Student
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com",
                DateOfBirth = new DateTime(1998, 5, 12)
            },
            new Student
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@test.com",
                DateOfBirth = new DateTime(2000, 1, 25)
            },
            new Student
            {
                Id = Guid.NewGuid(),
                FirstName = "Mahmoud",
                LastName = "Elgendi",
                Email = "mahmoud@test.com",
                DateOfBirth = new DateTime(1995, 3, 10)
            }
            ,
            new Student
            {
                Id = Guid.NewGuid(),
                FirstName = "Israa",
                LastName = "Maher",
                Email = "Israa@test.com",
                DateOfBirth = new DateTime(1993, 3, 10)
            }
        };

            context.Students.AddRange(students);
            await context.SaveChangesAsync();
        }
    }

}
