using System.Reflection.Metadata;
using Domain.Entities;
using Infrastructure.Persistence;
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
            return services;
        }
    }
}
