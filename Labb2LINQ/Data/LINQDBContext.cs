using Labb2LINQ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2LINQ.Data
{
    internal class LINQDBContext : DbContext
    {
        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = DESKTOP-M1V7EJK;Initial Catalog=LINQDBLabb;Integrated Security = True;TrustServerCertificate=True;");
        }
    }
}
