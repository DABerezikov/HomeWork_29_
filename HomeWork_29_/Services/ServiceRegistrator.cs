using HomeWork_29_.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HomeWork_29_.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
           .AddTransient<IDataService, DataService>()
           .AddTransient<IUserDialog, UserDialog>()
           .AddTransient<ISalesService, SalesService>()
           ;
    }
}
