using Application.Persistance.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance.Email;
using Infrastructure.Persistance;
using Infrastructure.Persistance.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfigurationManager conf)
        {
            services.AddScoped<SignInManager<User>>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<MeetUpperDbContext>()
                .AddDefaultTokenProviders();
            services.AddDbContext<MeetUpperDbContext>(options => 
                options.UseNpgsql(conf.GetConnectionString("Db"),
                    m => m.MigrationsAssembly(typeof(MeetUpperDbContext).Assembly.GetName().FullName)));
            services.AddHostedService<Migrator>();
            services.AddScoped<IEmailSender, EmailService>();
            services.Configure<IdentityOptions>(option =>
            {
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(120);
                option.Lockout.AllowedForNewUsers = false;
                option.Lockout.MaxFailedAccessAttempts = 6;
                
                option.User.RequireUniqueEmail = true;

                option.SignIn.RequireConfirmedPhoneNumber = false;
                option.SignIn.RequireConfirmedEmail = false;
                
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 8;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
            });
            var smtp = new SmtpConfiguration();
            conf.GetSection(key:"SMTP").Bind(smtp);
            services.AddSingleton(smtp);
            return services;
        }
    }
}
