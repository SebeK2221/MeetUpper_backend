using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Email;
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
            services.AddDbContext<MeetUpperDbContext>(options => 
                options.UseNpgsql(conf.GetConnectionString("Db"),
                    m => m.MigrationsAssembly(typeof(MeetUpperDbContext).Assembly.GetName().FullName)));
            services.AddHostedService<Migrator>();
            services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<MeetUpperDbContext>();
            services.AddScoped<IEmailSender, EmailSender>();
            // var smtp = new SmtpConfiguration();
            // conf.GetSection(key:"SMTP").Bind(smtp);
            // services.AddSingleton(smtp);
            services.Configure<SmtpConfiguration>(conf.GetSection("SMTP"));
            return services;
        }
    }
}
