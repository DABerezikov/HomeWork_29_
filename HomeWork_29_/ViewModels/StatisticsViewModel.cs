using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using MathCore.WPF.ViewModels;

namespace HomeWork_29_.ViewModels;

public class StatisticsViewModel:ViewModel
{
    private readonly IRepository<Buyer> _buyerRepository;
    private readonly IRepository<Product> _productsRepository;

    public StatisticsViewModel(IRepository<Buyer> buyerRepository, IRepository<Product> productsRepository)
    {
        _buyerRepository = buyerRepository;
        _productsRepository = productsRepository;
    }
}