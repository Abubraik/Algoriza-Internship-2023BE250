using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Repository.DataSeed
{
    public static class ModelBuilderExtensions
    {

        public static void DataSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>().HasData(
    new Specialization { SpecializationId = 1, Name = "Cardiology" },
    new Specialization { SpecializationId = 2, Name = "Neurology" },
    new Specialization { SpecializationId = 3, Name = "Oncology" },
    new Specialization { SpecializationId = 4, Name = "Pediatrics" },
    new Specialization { SpecializationId = 5, Name = "Gastroenterology" },
    new Specialization { SpecializationId = 6, Name = "Orthopedics" },
    new Specialization { SpecializationId = 7, Name = "Dermatology" },
    new Specialization { SpecializationId = 8, Name = "Endocrinology" },
    new Specialization { SpecializationId = 9, Name = "Ophthalmology" },
    new Specialization { SpecializationId = 10, Name = "Obstetrics and Gynecology" },
    new Specialization { SpecializationId = 11, Name = "Urology" },
    new Specialization { SpecializationId = 12, Name = "Psychiatry" },
    new Specialization { SpecializationId = 13, Name = "Anesthesiology" },
    new Specialization { SpecializationId = 14, Name = "Pulmonology" },
    new Specialization { SpecializationId = 15, Name = "Rheumatology" },
    new Specialization { SpecializationId = 16, Name = "Nephrology" },
    new Specialization { SpecializationId = 17, Name = "ENT (Ear, Nose, and Throat)" },
    new Specialization { SpecializationId = 18, Name = "Radiology" },
    new Specialization { SpecializationId = 19, Name = "Immunology" },
    new Specialization { SpecializationId = 20, Name = "Pathology" },
    new Specialization { SpecializationId = 21, Name = "General Surgery" },
    new Specialization { SpecializationId = 22, Name = "Plastic Surgery" },
    new Specialization { SpecializationId = 23, Name = "Neurosurgery" },
    new Specialization { SpecializationId = 24, Name = "Cardiothoracic Surgery" },
    new Specialization { SpecializationId = 25, Name = "Vascular Surgery" },
    new Specialization { SpecializationId = 26, Name = "Emergency Medicine" },
    new Specialization { SpecializationId = 27, Name = "Sports Medicine" },
    new Specialization { SpecializationId = 28, Name = "Geriatrics" },
    new Specialization { SpecializationId = 29, Name = "Hematology" },
    new Specialization { SpecializationId = 30, Name = "Infectious Disease" }
   );

            string ADMIN_ID = "02174cf0–9412–4cfe - afbf - 59f706d72cf6";

            //seed admin role
            modelBuilder.Entity<IdentityRole>().HasData(
          new IdentityRole { Id = "admin-role", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "admin-role" },
          new IdentityRole { Id = "patient-role", Name = "Patient", NormalizedName = "PATIENT", ConcurrencyStamp = "patient-role" },
          new IdentityRole { Id = "doctor-role", Name = "Doctor", NormalizedName = "DOCTOR", ConcurrencyStamp = "doctor-role" });
            //create user
            var appUser = new ApplicationUser
            {
                Id = ADMIN_ID,
                Email = "admin@vezeeta.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Vezeeta",
                UserName = "admin@vezeeta.com",
                NormalizedUserName = "admin@vezeeta.com",
            };

            //set user password
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "P@ssw0rd");

            //seed user
            modelBuilder.Entity<ApplicationUser>().HasData(appUser);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "admin-role",
                UserId = ADMIN_ID
            });
            //  var hasher = new PasswordHasher<IdentityUser>();

            //  var adminUser = new IdentityUser
            //  {
            //      Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
            //      UserName = "Admin",
            //      Email = "admin@vezeeta.com",
            //      PhoneNumber = "01066147039",
            //  };
            //  adminUser.PasswordHash = hasher.HashPassword(adminUser, "P@ssw0rd");
            //  modelBuilder.Entity<IdentityUser>().HasData(adminUser);


            //  modelBuilder.Entity<IdentityUserRole<string>>()
            //      .HasData(new IdentityUserRole<string>
            //      {
            //          RoleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
            //          UserId = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
            //      });



        }
        //public static void SeedAdmin()
        //{
        //    if (userManager.FindByEmailAsync("abc@xyz.com").Result == null)
        //    {
        //        IdentityUser user = new IdentityUser
        //        {
        //            UserName = "abc@xyz.com",
        //            Email = "abc@xyz.com"
        //        };

        //        IdentityResult result = userManager.CreateAsync(user, "PasswordHere").Result;

        //        if (result.Succeeded)
        //        {
        //            userManager.AddToRoleAsync(user, "Admin").Wait();
        //        }
        //    }

        //}
    }
}
