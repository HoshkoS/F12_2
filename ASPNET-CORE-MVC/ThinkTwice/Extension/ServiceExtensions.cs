using Domain.Repositories;
using Domain.Services.UserService;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace ThinkTwice.Extension;

public static class ServiceExtensions
{
    public static void AddServerDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ServerDbContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
    }

    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddSerilog(this IServiceCollection services)
    {
        services.AddLogging(loggingBuilder => { loggingBuilder.AddSeq(); });
    }

    public static void Add(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}