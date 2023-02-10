using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        var timer = Stopwatch.StartNew();
        _Logger.LogInformation("Инициализация БД...");
        _Logger.LogInformation("Удаление существующей БД...");
        await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
        _Logger.LogInformation("Удаление существующей БД выполнено за {0} мс", timer.ElapsedMilliseconds);
        //_db.Database.EnsureCreated();
        _Logger.LogInformation("Миграция БД...");
        await _db.Database.MigrateAsync();
        _Logger.LogInformation("Миграция БД выполнена за {0} мс", timer.ElapsedMilliseconds);
        if (await _db.Product.AnyAsync()) return;

        await InitializeProducts();
        await InitializeBuyers();
        await InitializeDeals();
        _Logger.LogInformation("Инициализация БД выполнена за {0} c", timer.Elapsed.Seconds);
    }

    private const int __ProductCount = 10;
    private Product[] _Product;

    private async Task InitializeProducts()
    {
        var timer = Stopwatch.StartNew();
        _Logger.LogInformation("Инициализация продуктов...");
        _Product = Enumerable.Range(1, __ProductCount)
            .Select(i => new Product
            {
                Name = $"Товар {i}",
                Code = i+10
            }).ToArray();

        await _db.Product.AddRangeAsync(_Product);
        await _db.SaveChangesAsync();
        _Logger.LogInformation("Инициализация продуктов выполнена за {0} мс", timer.ElapsedMilliseconds);
    }

    private const int __BuyerCount = 10;
    private Buyer[] _Buyers;

    private async Task InitializeBuyers()
    {
        var timer = Stopwatch.StartNew();
        _Logger.LogInformation("Инициализация покупателей...");
        _Buyers = Enumerable.Range(1, __BuyerCount)
            .Select(i => new Buyer
            {
                Name = $"Клиент-Имя {i}",
                Surname = $"Клиент-Фамилия {i}",
                Patronymic = $"Клиент-Отчество {i}",
                //Phone = $"Клиент-Телефон {i}",
                //Email = $"Клиент-Email{i}@mail.ru"
            }).ToArray();

        await _db.Buyers.AddRangeAsync(_Buyers);
        await _db.SaveChangesAsync();
        _Logger.LogInformation("Инициализация покупателей выполнена за {0} мс", timer.ElapsedMilliseconds);
    }

    private const int __DealCount = 1000;
    private async Task InitializeDeals()
    {
        var timer = Stopwatch.StartNew();
        _Logger.LogInformation("Инициализация сделок...");
        var rnd = new Random();
       
        var deals = Enumerable.Range(1, __DealCount)
            .Select(i => new Deal
            {
                
                Product = rnd.NextItem(_Product),
                Buyer = rnd.NextItem(_Buyers)
            });
        await _db.Deals.AddRangeAsync(deals);
        await _db.SaveChangesAsync();
        _Logger.LogInformation("Инициализация сделок выполнена за {0} мс", timer.ElapsedMilliseconds);
    }
}