﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vezeeta.Repository;

#nullable disable

namespace Vezeeta.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231208170926_alterBooking")]
    partial class alterBooking
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "admin-role",
                            ConcurrencyStamp = "admin-role",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "patient-role",
                            ConcurrencyStamp = "patient-role",
                            Name = "Patient",
                            NormalizedName = "PATIENT"
                        },
                        new
                        {
                            Id = "doctor-role",
                            ConcurrencyStamp = "doctor-role",
                            Name = "Doctor",
                            NormalizedName = "DOCTOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                            RoleId = "admin-role"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DoctorId")
                        .IsUnique();

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<int?>("DiscountCodeId")
                        .HasColumnType("int");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("FinalPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<int>("TimeSlotId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("DiscountCodeId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("TimeSlotId")
                        .IsUnique();

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.DaySchedule", b =>
                {
                    b.Property<int>("DayScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DayScheduleId"));

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<short>("DayOfWeek")
                        .HasColumnType("smallint");

                    b.HasKey("DayScheduleId");

                    b.HasIndex("AppointmentId");

                    b.ToTable("DaySchedules");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.DiscountCode", b =>
                {
                    b.Property<int>("DiscountCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiscountCodeId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("DiscountType")
                        .HasColumnType("smallint");

                    b.Property<decimal>("DiscountValue")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfRequiredBookings")
                        .HasColumnType("int");

                    b.HasKey("DiscountCodeId");

                    b.ToTable("DiscountCode");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PatientFeedback")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Specialization", b =>
                {
                    b.Property<int>("SpecializationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpecializationId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpecializationId");

                    b.ToTable("Specializations");

                    b.HasData(
                        new
                        {
                            SpecializationId = 1,
                            Name = "Cardiology"
                        },
                        new
                        {
                            SpecializationId = 2,
                            Name = "Neurology"
                        },
                        new
                        {
                            SpecializationId = 3,
                            Name = "Oncology"
                        },
                        new
                        {
                            SpecializationId = 4,
                            Name = "Pediatrics"
                        },
                        new
                        {
                            SpecializationId = 5,
                            Name = "Gastroenterology"
                        },
                        new
                        {
                            SpecializationId = 6,
                            Name = "Orthopedics"
                        },
                        new
                        {
                            SpecializationId = 7,
                            Name = "Dermatology"
                        },
                        new
                        {
                            SpecializationId = 8,
                            Name = "Endocrinology"
                        },
                        new
                        {
                            SpecializationId = 9,
                            Name = "Ophthalmology"
                        },
                        new
                        {
                            SpecializationId = 10,
                            Name = "Obstetrics and Gynecology"
                        },
                        new
                        {
                            SpecializationId = 11,
                            Name = "Urology"
                        },
                        new
                        {
                            SpecializationId = 12,
                            Name = "Psychiatry"
                        },
                        new
                        {
                            SpecializationId = 13,
                            Name = "Anesthesiology"
                        },
                        new
                        {
                            SpecializationId = 14,
                            Name = "Pulmonology"
                        },
                        new
                        {
                            SpecializationId = 15,
                            Name = "Rheumatology"
                        },
                        new
                        {
                            SpecializationId = 16,
                            Name = "Nephrology"
                        },
                        new
                        {
                            SpecializationId = 17,
                            Name = "ENT (Ear, Nose, and Throat)"
                        },
                        new
                        {
                            SpecializationId = 18,
                            Name = "Radiology"
                        },
                        new
                        {
                            SpecializationId = 19,
                            Name = "Immunology"
                        },
                        new
                        {
                            SpecializationId = 20,
                            Name = "Pathology"
                        },
                        new
                        {
                            SpecializationId = 21,
                            Name = "General Surgery"
                        },
                        new
                        {
                            SpecializationId = 22,
                            Name = "Plastic Surgery"
                        },
                        new
                        {
                            SpecializationId = 23,
                            Name = "Neurosurgery"
                        },
                        new
                        {
                            SpecializationId = 24,
                            Name = "Cardiothoracic Surgery"
                        },
                        new
                        {
                            SpecializationId = 25,
                            Name = "Vascular Surgery"
                        },
                        new
                        {
                            SpecializationId = 26,
                            Name = "Emergency Medicine"
                        },
                        new
                        {
                            SpecializationId = 27,
                            Name = "Sports Medicine"
                        },
                        new
                        {
                            SpecializationId = 28,
                            Name = "Geriatrics"
                        },
                        new
                        {
                            SpecializationId = 29,
                            Name = "Hematology"
                        },
                        new
                        {
                            SpecializationId = 30,
                            Name = "Infectious Disease"
                        });
                });

            modelBuilder.Entity("Vezeeta.Core.Models.TimeSlot", b =>
                {
                    b.Property<int>("TiemSlotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TiemSlotId"));

                    b.Property<int>("DayScheduleId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<bool>("IsBooked")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("TiemSlotId");

                    b.HasIndex("DayScheduleId");

                    b.ToTable("TimeSlots");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Users.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("Gender")
                        .HasColumnType("smallint");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.UseTptMappingStrategy();

                    b.HasData(
                        new
                        {
                            Id = "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a28e9d70-7100-42c4-be9f-af6b216ba451",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@vezeeta.com",
                            EmailConfirmed = true,
                            FirstName = "Admin",
                            Gender = (short)0,
                            LastName = "Vezeeta",
                            LockoutEnabled = false,
                            NormalizedUserName = "admin@vezeeta.com",
                            PasswordHash = "AQAAAAIAAYagAAAAEIjLiksecXCuw6+U+aWOuDsO10jhhqLM+UP/ZrRgU7B2eQ0ZpPJkTNznmxKWw60HTw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c0051bff-1b70-4a64-b8fa-13fc0b063c73",
                            TwoFactorEnabled = false,
                            UserName = "admin@vezeeta.com"
                        });
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Users.Doctor", b =>
                {
                    b.HasBaseType("Vezeeta.Core.Models.Users.ApplicationUser");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpecializationId")
                        .HasColumnType("int");

                    b.HasIndex("SpecializationId");

                    b.ToTable("Doctors", (string)null);
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Users.Patient", b =>
                {
                    b.HasBaseType("Vezeeta.Core.Models.Users.ApplicationUser");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Patients", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Appointment", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Users.Doctor", "Doctor")
                        .WithOne("Appointments")
                        .HasForeignKey("Vezeeta.Core.Models.Appointment", "DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Booking", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.DiscountCode", "DiscountCode")
                        .WithMany("Bookings")
                        .HasForeignKey("DiscountCodeId");

                    b.HasOne("Vezeeta.Core.Models.Users.Doctor", "Doctor")
                        .WithMany("Bookings")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.Users.Patient", "Patient")
                        .WithMany("Bookings")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.TimeSlot", "TimeSlot")
                        .WithOne()
                        .HasForeignKey("Vezeeta.Core.Models.Booking", "TimeSlotId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DiscountCode");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("TimeSlot");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.DaySchedule", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Appointment", "Appointment")
                        .WithMany("DaySchedules")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Feedback", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Booking", "Booking")
                        .WithOne("Feedback")
                        .HasForeignKey("Vezeeta.Core.Models.Feedback", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.Users.Doctor", "Doctor")
                        .WithMany("Feedbacks")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.Users.Patient", "Patient")
                        .WithMany("Feedbacks")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.TimeSlot", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.DaySchedule", "DaySchedule")
                        .WithMany("TimeSlots")
                        .HasForeignKey("DayScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DaySchedule");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Users.Doctor", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Users.ApplicationUser", null)
                        .WithOne()
                        .HasForeignKey("Vezeeta.Core.Models.Users.Doctor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vezeeta.Core.Models.Specialization", "Specialization")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Users.Patient", b =>
                {
                    b.HasOne("Vezeeta.Core.Models.Users.ApplicationUser", null)
                        .WithOne()
                        .HasForeignKey("Vezeeta.Core.Models.Users.Patient", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Appointment", b =>
                {
                    b.Navigation("DaySchedules");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Booking", b =>
                {
                    b.Navigation("Feedback");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.DaySchedule", b =>
                {
                    b.Navigation("TimeSlots");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.DiscountCode", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Specialization", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Users.Doctor", b =>
                {
                    b.Navigation("Appointments")
                        .IsRequired();

                    b.Navigation("Bookings");

                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("Vezeeta.Core.Models.Users.Patient", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Feedbacks");
                });
#pragma warning restore 612, 618
        }
    }
}
