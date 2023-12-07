using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Repository;
using Vezeeta.Repository.Repositories;
using Vezeeta.Service.Services;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Services;
using Vezeeta.Sevices.Services.Interfaces;


namespace Vezeeta.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaulConnection"),
            b=> b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            ));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 4;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            builder.Services.AddIdentityCore<Doctor>().AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddIdentityCore<Patient>().AddEntityFrameworkStores<ApplicationDbContext>();
            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            //    options.Cookie.IsEssential = true;
            //    options.SlidingExpiration = true; 
            //    options.Cookie.SameSite = SameSiteMode.Strict;
            //    options.Cookie.MaxAge = null;
            //});
            //Add Email required
            //builder.Services.Configure<IdentityOptions>(options => options.SignIn.RequireConfirmedEmail = true);

            //Add Email Config
            var emailConfig = builder.Configuration
                .GetSection("EmailConfig")
                .Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();



            //builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>) );
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserManagementSevice, UserManagementSevice>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IPatientService,PatientService>();

            builder.Services.AddAutoMapper(typeof(Vezeeta.Sevices.Helpers.Mappers));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}