using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using HomeWork_29_.Infrastructure.DebugServices;
using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using MathCore.WPF.ViewModels;

namespace HomeWork_29_.ViewModels;

public class ProductsViewModel:ViewModel
{
    private readonly IRepository<Product> _Product;

    #region ProductFilter : string - Искомое слово

    /// <summary>Искомое слово</summary>
    private string _ProductFilter;

    /// <summary>Искомое слово</summary>
    public string ProductFilter
    {
        get => _ProductFilter;
        set
        {
           if( Set(ref _ProductFilter, value))
               _ProductViewSource.View.Refresh();

        }
    }

    #endregion

    public ICollectionView ProductView => _ProductViewSource.View;

    private CollectionViewSource _ProductViewSource;
    
    public IEnumerable<Product> Products => _Product.Items.ToList();

    public ProductsViewModel()
    :this(new DebugProductRepository())
    {
        if (!App.IsDesignTime)
        {
            throw new InvalidOperationException(
                "Данный конструктор не предназначен для использования вне дизайнера студии");
        }

        
    }

    public ProductsViewModel(IRepository<Product> product)
    {
        _Product = product;

        _ProductViewSource = new CollectionViewSource
        {
            Source = _Product.Items.ToArray(),
            SortDescriptions =
            {
                new SortDescription(nameof(Product.Name), ListSortDirection.Ascending)
            }
        };

        _ProductViewSource.Filter += OnProductFilter;
    }

    private void OnProductFilter(object sender, FilterEventArgs e)
    {
        if (!(e.Item is Product product) || string.IsNullOrEmpty(ProductFilter)) return;

        if (!product.Name.Contains(ProductFilter))
            e.Accepted = false;
       
    }
}