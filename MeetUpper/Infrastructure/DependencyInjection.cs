using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Email;
using Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MeetUpperDbContext>(options => 
                options.UseNpgsql(configuration.GetConnectionString("Db"),
                    m => m.MigrationsAssembly(typeof(MeetUpperDbContext).Assembly.GetName().FullName)));
            
            services.AddHostedService<Migrator>();
            
            services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<MeetUpperDbContext>();
            
            services.AddFluentEmail(configuration);
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
