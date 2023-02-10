using HomeWork_29_.Services.Interfaces;
using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork_29_.Services;

public class SalesService : ISalesService
{
    private readonly IRepository<Product> _products;
    private readonly IRepository<Deal> _deals;

    public IEnumerable<Deal> Deals => _deals.Items;

    public SalesService(IRepository<Product> products, IRepository<Deal> deals)
    {
        _products = products;
        _deals = deals;
    }

    public async Task<Deal> MakeADeal(string productName, Buyer buyer)
    {

        var product = await _products.Items.FirstOrDefaultAsync(p => p.Name == productName).ConfigureAwait(false);


        if (product is null) return null;
        var deal = new Deal
        {
            Buyer = buyer,
            Product = product
        };
        return await _deals.AddAsync(deal);
    }

}