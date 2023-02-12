using System;
using System.Collections.Generic;
using System.Linq;
using HomeWork_29_.Infrastructure.DebugServices;
using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using MathCore.WPF.ViewModels;

namespace HomeWork_29_.ViewModels;

public class ProductsViewModel:ViewModel
{
    private readonly IRepository<Product> _Product;

    public IQueryable<Product> Products => _Product.Items;

    public ProductsViewModel()
    {
        if (!App.IsDesignTime)
        {
            throw new InvalidOperationException(
                "Данный конструктор не предназначен для использования вне дизайнера студии");
        }

        _Product = new DebugProductRepository();
    }

    public ProductsViewModel(IRepository<Product> product)
    {
        _Product = product;
    }
}