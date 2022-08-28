using Core;
using DAL.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DependencyRegisterer
    {
        public static void Register(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<DbContext, ChatContext>(builder => builder.UseSqlServer(configuration.GetConnectionString("ChatDb")));
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            serviceCollection.AddScoped<IRepository<User>, UserRepository>();
            serviceCollection.AddScoped<IRepository<Message>, MessageRepository>();
            serviceCollection.AddScoped<IRepository<Group>, GroupRepository>();
        }
    }
}
