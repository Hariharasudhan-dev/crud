using crud.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace crud
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Student> Student { get; set; }

        public DbSet<Office> Office { get; set; }

        public DbSet<Questions>Questions { get; set; }

        public DbSet<Answers>Answers { get; set; }
       
    }
}
