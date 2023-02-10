using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HomeWork_29_DB;

public static class RepositoryRegistrator
{
    public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
        .AddTransient<IRepository<Product>, DbRepository<Product>>()
        .AddTransient<IRepository<Buyer>, DbRepository<Buyer>>()
        .AddTransient<IRepository<Deal>, DealsRepository>()
        ;
}