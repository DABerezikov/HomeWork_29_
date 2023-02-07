using Microsoft.Extensions.DependencyInjection;

namespace HomeWork_29_.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
