using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HomeWork_29_.Infrastructure.Commands;
using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using LambdaCommandAsync = MathCore.WPF.Commands.LambdaCommandAsync;

namespace HomeWork_29_.ViewModels;

public class StatisticsViewModel:ViewModel
{
    private readonly IRepository<Buyer> _buyerRepository;
    private readonly IRepository<Product> _productsRepository;
    private readonly IRepository<Deal> _dealsRepository;

    #region ProductCount : int - Заголовок окна

    /// <summary>Заголовок окна</summary>
    private int _ProductCount;

    /// <summary>Заголовок окна</summary>
    public int ProductCount { get => _ProductCount; private set => Set(ref _ProductCount, value); }

    #endregion

    #region Command ComputeStatisticsCommand - Вычислить статистику

    /// <summary> Вычислить статистику </summary>
    private ICommand _ComputeStatisticsCommand;

    /// <summary> Вычислить статистику </summary>
    public ICommand ComputeStatisticsCommand => _ComputeStatisticsCommand
        ??= new LambdaCommandAsync(OnComputeStatisticsCommandExecuted, CanComputeStatisticsCommandExecute);

    /// <summary> Проверка возможности выполнения - Вычислить статистику </summary>
    private bool CanComputeStatisticsCommandExecute() => true;

    /// <summary> Логика выполнения - Вычислить статистику </summary>
    private async Task OnComputeStatisticsCommandExecuted()
    {
        ProductCount = await _productsRepository.Items.CountAsync();

        var deals = _dealsRepository.Items;
        var bestsellers = await deals.GroupBy(deal => deal.Products)
            .Select(product_deals => new { Product = product_deals.Key, Count = product_deals.Count() })
            .OrderByDescending(product => product.Count)
            .Take(5)
            .ToArrayAsync();
    }

    #endregion

    public StatisticsViewModel(
        IRepository<Buyer> buyerRepository,
        IRepository<Product> productsRepository,
        IRepository<Deal> dealsRepository)
    {
        _buyerRepository = buyerRepository;
        _productsRepository = productsRepository;
        _dealsRepository = dealsRepository;
    }
}