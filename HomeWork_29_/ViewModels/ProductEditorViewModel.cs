using System;
using HomeWork_29_DB.Entityes;
using MathCore.ViewModels;

namespace HomeWork_29_.ViewModels;

public class ProductEditorViewModel : ViewModel
{
    #region Name : string - Название продукта

    /// <summary>Название продукта</summary>
    private string _Name;

    /// <summary>Название продукта</summary>
    public string Name
    {
        get => _Name;
        set => Set(ref _Name, value);
    }

    #endregion

    #region ProductId : int - Идентификатор книги

   

    /// <summary>Идентификатор книги</summary>
    public int ProductId { get; }
    

    #endregion

    public ProductEditorViewModel()
:this(new Product{ Id = 1, Name = "Продукт!"})
    {
        if (!App.IsDesignTime)
            throw new InvalidOperationException("Не для рантайма");

    }

    public ProductEditorViewModel(Product product)
    {
        ProductId = product.Id;
        Name = product.Name;

    }
}