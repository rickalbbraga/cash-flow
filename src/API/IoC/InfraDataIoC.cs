using Data.Context;
using Data.Repositories;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.IoC
{
    public static class InfraDataIoC
    {
        public static void AddInfraDataConfigurations(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetSection("SqLiteConnectionString").Value));

            serviceCollection.AddScoped<IEntryRepository, EntryRepository>();
        }
    }
}