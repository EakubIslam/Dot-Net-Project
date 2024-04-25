using CSharpFinalProject.Seed;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFinalProject
{
    public class AttendanceDbContext: DbContext
    {
        private readonly string _connectionString;
        public AttendanceDbContext()
        {
            _connectionString = "Server=EAKUB\\SQLEXPRESS;Database=FinalProject;User Id=csharppractice;Password=123456;TrustServerCertificate=True";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //change the database table name  as Topics
            modelBuilder.Entity<Admin>().HasData(AdminSeed.Admins);


            base.OnModelCreating(modelBuilder);
        }
        // Define a DbSet for each entity type
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ClassSchedule> ClassSchedules { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}

