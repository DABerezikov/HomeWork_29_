using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using HomeWork_29_.Infrastructure.DebugServices;
using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_29_.ViewModels;

public class ProductsViewModel:ViewModel
{
    private readonly IRepository<Product> _Product;

    #region Products : ObservableCollection<Product> - Description

    /// <summary>Description</summary>
    private ObservableCollection<Product> _Products;

    /// <summary>Description</summary>
    public ObservableCollection<Product> Products
    {
        get => _Products;
        set
        {
            if (Set(ref _Products, value))
                _ProductViewSource.Source = value;
            //_ProductViewSource.View.Refresh();
            OnPropertyChanged(nameof(ProductView));

        } 
    }

    #endregion

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

    private readonly CollectionViewSource _ProductViewSource;


    #region Command LoadDataCommand - Команда для загрызки данных из репозитория

    /// <summary> Команда для загрызки данных из репозитория </summary>
    private ICommand _LoadDataCommand;

    /// <summary> Команда для загрызки данных из репозитория </summary>
    public ICommand LoadDataCommand => _LoadDataCommand
        ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);

    /// <summary> Проверка возможности выполнения - Команда для загрызки данных из репозитория </summary>
    private bool CanLoadDataCommandExecute() => true;

    /// <summary> Логика выполнения - Команда для загрызки данных из репозитория </summary>
    private async Task OnLoadDataCommandExecuted()
    {
        //Products = (await _Product.Items.ToArrayAsync()).ToObservableCollection();
        Products = new ObservableCollection<Product>(await _Product.Items.ToArrayAsync());
    }

    #endregion

    public ProductsViewModel()
    :this(new DebugProductRepository())
    {
        if (!App.IsDesignTime)
        {
            throw new InvalidOperationException(
                "Данный конструктор не предназначен для использования вне дизайнера студии");
        }

        _ = OnLoadDataCommandExecuted();
    }

    public ProductsViewModel(IRepository<Product> product)
    {
        _Product = product;

        _ProductViewSource = new CollectionViewSource
        {
            
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