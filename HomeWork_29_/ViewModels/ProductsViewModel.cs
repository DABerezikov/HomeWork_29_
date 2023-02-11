using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using MathCore.WPF.ViewModels;

namespace HomeWork_29_.ViewModels;

public class ProductsViewModel:ViewModel
{
    private readonly IRepository<Product> _productsRepository;

    public ProductsViewModel(IRepository<Product> productsRepository)
    {
        _productsRepository = productsRepository;
    }
}