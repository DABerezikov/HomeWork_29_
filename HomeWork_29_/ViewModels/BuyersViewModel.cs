using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using MathCore.WPF.ViewModels;

namespace HomeWork_29_.ViewModels;

public class BuyersViewModel :ViewModel
{
    private readonly IRepository<Buyer> _buyerRepository;

    public BuyersViewModel(IRepository<Buyer> buyerRepository)
    {
        _buyerRepository = buyerRepository;
    }
}