using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;

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
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.dateOfBirth)
                .HasColumnType("date");

            modelBuilder.Entity<Patient>(e => e.ToTable("Patients"));
            modelBuilder.Entity<Doctor>(e => e.ToTable("Doctors"));
            //modelBuilder.Entity<TimeSlot>()
            //    .Property(e => e.startTime)
            //    .HasColumnType("time");

            //modelBuilder.Entity<TimeSlot>()
            //    .Property(e => e.endTime)
                //.HasColumnType("time");

            modelBuilder.Entity<Booking>()
                .HasOne(e => e.timeSlot)
                .WithMany()
                .HasForeignKey(e => e.timeSlotId);

            //modelBuilder.Entity<Feedback>()
            //    .HasOne(e => e.booking)
            //    .WithOne();
            //modelBuilder.Entity<DoctorUser>()
            //    .HasMany(b => b.feedbacks)
            //    .WithOne();
            //modelBuilder.Entity<Feedbacks>()
            //    .HasOne(b => b.patient)
            //    .WithMany()
            //    .HasForeignKey(e => e.patientId);
            //modelBuilder.Entity<Feedbacks>()
            //    .HasOne(b => b.doctor)
            //    .WithMany()
            //    .HasForeignKey(e => e.doctorId);
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }


    }
}
