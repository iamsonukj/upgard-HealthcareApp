using Microsoft.EntityFrameworkCore;
using HealthcareApp.Entities.Models;

namespace HealthcareApp.DAL.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
       
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique Phone Number
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.PhoneNumber)
                .IsUnique();

            //  Prevent duplicate appointment
            modelBuilder.Entity<Appointment>()
                .HasIndex(a => new { a.DoctorId, a.Date, a.TimeSlot })
                .IsUnique();
        }
    }
}