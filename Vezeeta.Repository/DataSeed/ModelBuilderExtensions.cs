using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;

namespace Vezeeta.Repository.DataSeed
{
    public static class ModelBuilderExtensions
    {
        public static void DataSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>().HasData(
new Specialization {specializationId= 1, name = "Cardiology" },
    new Specialization {specializationId= 2, name = "Neurology" },
    new Specialization {specializationId= 3, name = "Oncology" },
    new Specialization {specializationId= 4, name = "Pediatrics" },
    new Specialization {specializationId= 5, name = "Gastroenterology" },
    new Specialization {specializationId= 6, name = "Orthopedics" },
    new Specialization {specializationId= 7, name = "Dermatology" },
    new Specialization {specializationId= 8, name = "Endocrinology" },
    new Specialization {specializationId= 9, name = "Ophthalmology" },
    new Specialization {specializationId= 10, name = "Obstetrics and Gynecology" },
    new Specialization {specializationId= 11, name = "Urology" },
    new Specialization {specializationId= 12, name = "Psychiatry" },
    new Specialization {specializationId= 13, name = "Anesthesiology" },
    new Specialization {specializationId= 14, name = "Pulmonology" },
    new Specialization {specializationId= 15, name = "Rheumatology" },
    new Specialization {specializationId= 16, name = "Nephrology" },
    new Specialization {specializationId= 17, name = "ENT (Ear, Nose, and Throat)" },
    new Specialization {specializationId= 18, name = "Radiology" },
    new Specialization {specializationId= 19, name = "Immunology" },
    new Specialization {specializationId= 20, name = "Pathology" },
    new Specialization {specializationId= 21, name = "General Surgery" },
    new Specialization {specializationId= 22, name = "Plastic Surgery" },
    new Specialization {specializationId= 23, name = "Neurosurgery" },
    new Specialization {specializationId= 24, name = "Cardiothoracic Surgery" },
    new Specialization {specializationId= 25, name = "Vascular Surgery" },
    new Specialization {specializationId= 26, name = "Emergency Medicine" },
    new Specialization {specializationId= 27, name = "Sports Medicine" },
    new Specialization {specializationId= 28, name = "Geriatrics" },
    new Specialization {specializationId= 29, name = "Hematology" },
    new Specialization {specializationId= 30, name = "Infectious Disease" }
   );
            modelBuilder.Entity<IdentityRole>().HasData(
           new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
           new IdentityRole { Id = "2", Name = "Patient", NormalizedName = "PATIENT" },
           new IdentityRole { Id = "3", Name = "Doctor", NormalizedName = "DOCTOR" });

            var hasher = new PasswordHasher<IdentityUser>();

            var adminUser = new IdentityUser
            {
                UserName = "Admin",
                Email = "admin@vezeeta.com",
                PhoneNumber = "01066147039",
            }; 
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "YourStrongAdmin;Password");
            modelBuilder.Entity<IdentityUser>().HasData(adminUser);

        }

    }
}
