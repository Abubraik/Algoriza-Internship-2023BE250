﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;
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
                .Property(e => e.startTime)
                .HasColumnType("time")
                .HasConversion(timeOnlyConverter);
            modelBuilder.Entity<TimeSlot>()
                .Property(e => e.endTime)
                .HasColumnType("time")
                .HasConversion(timeOnlyConverter);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.dateOfBirth)
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
                .Property(e => e.dateOfBirth)
                .HasColumnType("date");
            modelBuilder.Entity<Booking>()
            .HasOne(b => b.patient)
            .WithMany(p => p.bookings)
            .HasForeignKey(b => b.patientId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
            .HasOne(b => b.timeSlot)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(b=>b.doctor)
                .WithMany(b=>b.feedbacks)
                .HasForeignKey(b=>b.doctorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Feedback>()
                .HasOne(b => b.patient)
                .WithMany(b => b.feedbacks)
                .HasForeignKey(b => b.patientId)
                .OnDelete(DeleteBehavior.Restrict);

            #region
            //modelBuilder.Entity<TimeSlot>()
            //    .Property(e => e.startTime)
            //    .HasColumnType("time");

            //modelBuilder.Entity<TimeSlot>()
            //    .Property(e => e.endTime)
            //.HasColumnType("time");

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
            #endregion
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }


    }
}
