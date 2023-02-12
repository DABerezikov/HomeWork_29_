using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HomeWork_29_.Infrastructure.Commands;
using HomeWork_29_.Models;
using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using LambdaCommandAsync = MathCore.WPF.Commands.LambdaCommandAsync;

namespace HomeWork_29_.ViewModels;

public class StatisticsViewModel:ViewModel
{
    private readonly IRepository<Buyer> _Buyer;
    private readonly IRepository<Product> _Product;
    private readonly IRepository<Deal> _Deals;

    public ObservableCollection<BestSellersInfo> BestSellers { get; } = new ();


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
        await ComputeDealsStatisticAsync();

        
    }

    private async Task ComputeDealsStatisticAsync()
    {
        
        var bestsellers_query = _Deals.Items
            .GroupBy(deal => deal.Products.Id)
            .Select(deals => new  { ProductID = deals.Key, Count = deals.Count() })
            .OrderByDescending(product => product.Count)
            .Take(5)
            .Join(_Product.Items,
                deal =>deal.ProductID,
                product=> product.Id,
                (deal, product) => new BestSellersInfo { Product = product, SellCount = deal.Count});
         BestSellers.AddClear(await bestsellers_query.ToArrayAsync());
         
         //BestSellers.Clear();
         //foreach (var bestseller in await bestsellers_query.ToArrayAsync())
        //{
        //    BestSellers.Add(bestseller);
        //}
    }

    #endregion

    public StatisticsViewModel(
        IRepository<Buyer> buyer,
        IRepository<Product> product,
        IRepository<Deal> deals)
    {
        _Buyer = buyer;
        _Product = product;
        _Deals = deals;
    }
}