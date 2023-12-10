using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
            options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaulConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            ));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 4;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey
                    (System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
                };
            });


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
            builder.Services.AddScoped<IUserManagementService, UserManagementSevice>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IDiscountCodeService, DiscountCodeService>();


            builder.Services.AddAutoMapper(typeof(Sevices.Helpers.Mappers));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}