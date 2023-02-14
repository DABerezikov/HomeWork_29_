using HomeWork_29_.Infrastructure.DebugServices;
using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using HomeWork_29_.Services;
using HomeWork_29_.Services.Interfaces;

namespace HomeWork_29_.ViewModels;

public class ProductsViewModel : ViewModel
{
    private readonly IRepository<Product> _Product;
    private readonly IUserDialog _UserDialog;

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
            if (Set(ref _ProductFilter, value))
                _ProductViewSource.View.Refresh();

        }
    }

    #endregion

    public ICollectionView ProductView => _ProductViewSource.View;

    private readonly CollectionViewSource _ProductViewSource;

    #region SelectedProduct : Product - Выбранный продукт

    /// <summary>Выбранный продукт</summary>
    private Product _SelectedProduct;

    /// <summary>Выбранный продукт</summary>
    public Product SelectedProduct
    {
        get => _SelectedProduct;
        set => Set(ref _SelectedProduct, value);
    }

    #endregion

    #region Command AddNewProductCommand - Добавление нового продукта

    /// <summary> Добавление нового продукта </summary>
    private ICommand _AddNewProductCommand;

    /// <summary> Добавление нового продукта </summary>
    public ICommand AddNewProductCommand => _AddNewProductCommand
        ??= new LambdaCommand(OnAddNewProductCommandExecuted, CanAddNewProductCommandExecute);

    /// <summary> Проверка возможности выполнения - Добавление нового продукта </summary>
    private bool CanAddNewProductCommandExecute() => true;

    /// <summary> Логика выполнения - Добавление нового продукта </summary>
    private void OnAddNewProductCommandExecuted()
    {
        var new_product = new Product();
        if(!_UserDialog.Edit(new_product)) return;
        _Products.Add(_Product.Add(new_product));

        SelectedProduct = new_product;
    }

    #endregion

    #region Command RemoveProductCommand - Удаление указанного продукта

    /// <summary> Удаление указанного продукта </summary>
    private ICommand _RemoveProductCommand;

    /// <summary> Удаление указанного продукта </summary>
    public ICommand RemoveProductCommand => _RemoveProductCommand
        ??= new LambdaCommand<Product>(OnRemoveProductCommandExecuted, CanRemoveProductCommandExecute);

    /// <summary> Проверка возможности выполнения - Удаление указанного продукта </summary>
    private bool CanRemoveProductCommandExecute(Product p) => p !=null || SelectedProduct !=null;

    /// <summary> Логика выполнения - Удаление указанного продукта </summary>
    private void OnRemoveProductCommandExecuted(Product p)
    {
        var product_to_remove = p ?? SelectedProduct;

        if (!_UserDialog.ConfirmInformation($"Вы хотите удалить продукт {product_to_remove.Name}?", "Удаление продукта"))
            return;

        _Product.Remove(product_to_remove.Id);
        Products.Remove(product_to_remove);
        if (ReferenceEquals(SelectedProduct, product_to_remove))
            SelectedProduct = null;
    }

    #endregion

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
    : this(new DebugProductRepository(), new UserDialog())
    {
        if (!App.IsDesignTime)
        {
            throw new InvalidOperationException(
                "Данный конструктор не предназначен для использования вне дизайнера студии");
        }

        _ = OnLoadDataCommandExecuted();
    }

    public ProductsViewModel(IRepository<Product> product, IUserDialog userDialog)
    {
        _Product = product;
        _UserDialog = userDialog;

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