using System;
using System.Linq;
using System.Threading.Tasks;
using HomeWork_29_.Services;
using HomeWork_29_DB.Context;
using HomeWork_29_DB.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeWork_29_.Data;

public class DBInitializer
{
    private readonly HW_29_DB _db;
    private readonly ILogger<DBInitializer> _Logger;

    public DBInitializer(HW_29_DB db, ILogger <DBInitializer> logger)
    {
        _db = db;
        _Logger = logger;
    }

    public async Task InitialazeAsync()
    {
        await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
        //_db.Database.EnsureCreated();
       await _db.Database.MigrateAsync();

       if (await _db.Products.AnyAsync()) return;

       await InitializeBuyers();
       await InitializeProducts();
       await InitializeDeals();

    }

    private const int __ProductCount = 10;
    private Product[] _Products;

    private async Task InitializeProducts()
    {
        
        _Products = Enumerable.Range(1, __ProductCount)
            .Select(i => new Product
            {
                Name = $"Товар {i}",
                Code = i+10
            }).ToArray();

        await _db.Products.AddRangeAsync(_Products);
        await _db.SaveChangesAsync();
    }

    private const int __BuyerCount = 10;
    private Buyer[] _Buyers;

    private async Task InitializeBuyers()
    {
       
        _Buyers = Enumerable.Range(1, __BuyerCount)
            .Select(i => new Buyer
            {
                Name = $"Клиент-Имя {i}",
                Surname = $"Клиент-Фамилия {i}",
                Patronymic = $"Клиент-Отчество {i}",
                Phone = $"Клиент-Телефон {i}",
                Email = $"Клиент-Email{i}@mail.ru"
            }).ToArray();

        await _db.Buyer.AddRangeAsync(_Buyers);
        await _db.SaveChangesAsync();
    }

    private const int __DealCount = 1000;
    private async Task InitializeDeals()
    {
        var rnd = new Random();
       
        var deals = Enumerable.Range(1, __DealCount)
            .Select(i => new Deal
            {
                
                Products = { rnd.NextItem(_Products), rnd.NextItem(_Products), rnd.NextItem(_Products), rnd.NextItem(_Products)},
                Buyer = rnd.NextItem(_Buyers)
            });
        await _db.Deals.AddRangeAsync(deals);
        await _db.SaveChangesAsync();
    }
}