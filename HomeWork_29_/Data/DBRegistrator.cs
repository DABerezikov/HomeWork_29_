using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using HomeWork_29_DB.Context;

namespace HomeWork_29_.Data;

public static class DBRegistrator
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
        .AddDbContext<HW_29_DB>(opt =>
        {
            var type = configuration["Type"];
            switch (type)
            {
                case null: throw new InvalidOperationException("Не определён тип БД");

                default: throw new InvalidOperationException($"Тип подключения {type} не поддерживается");

                case "MSSQL":
                    opt.UseSqlServer(configuration.GetConnectionString(type));
                    break;
                case "PostgreSQL":
                    opt.UseNpgsql(configuration.GetConnectionString(type));
                    break;
                case "InMemory":
                    opt.UseInMemoryDatabase("HW_29.db");
                    break;

            }
        })
        .AddTransient<DBInitializer>()
    ;
}