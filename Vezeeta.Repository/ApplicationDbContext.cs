using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Repository.DataSeed;

namespace Vezeeta.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure DateOnlyProperty to store only the date part
            modelBuilder.DataSeed();



            //modelBuilder.RolesSeed();
            #region Conversion
            var decimalConverter = new ValueConverter<decimal, decimal>(
                model => Math.Round(model, 2),  // Round to 2 decimal places when saving to the database
                provider => provider            // No operation when reading from the database
            );


            var timeOnlyConverter = new ValueConverter<TimeOnly,TimeSpan>
                (timeOnly => timeOnly.ToTimeSpan(),
                timeSpan => TimeOnly.FromTimeSpan(timeSpan));
        
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime));

            modelBuilder.Entity<TimeSlot>()
                .Property(e => e.StartTime)
                .HasColumnType("time")
                .HasConversion(timeOnlyConverter);
            modelBuilder.Entity<TimeSlot>()
                .Property(e => e.EndTime)
                .HasColumnType("time")
                .HasConversion(timeOnlyConverter);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasConversion(dateOnlyConverter);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                    {
                        property.SetValueConverter(decimalConverter);
                        property.SetPrecision(18);  // Set the desired precision
                        property.SetScale(2);       // Set the desired scale
                    }
                }
            }

            #endregion

            modelBuilder.Entity<Patient>(e => e.ToTable("Patients"));
            modelBuilder.Entity<Doctor>(e => e.ToTable("Doctors"));

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.DateOfBirth)
                .HasColumnType("date");
            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Patient)
            .WithMany(p => p.Bookings)
            .HasForeignKey(b => b.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
            .HasOne(b => b.TimeSlot)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Doctor>()
            //    .HasOne(b => b.Appointments)
            //    .WithOne();

            modelBuilder.Entity<Feedback>()
                .HasOne(b=>b.Doctor)
                .WithMany(b=>b.Feedbacks)
                .HasForeignKey(b=>b.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Feedback>()
                .HasOne(b => b.Patient)
                .WithMany(b => b.Feedbacks)
                .HasForeignKey(b => b.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

 
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<DaySchedule> DaySchedules { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }


    }
}
