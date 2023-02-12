using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeWork_29_.Infrastructure.DebugServices;

public class DebugProductRepository : IRepository<Product>
{
    public DebugProductRepository()
    {
        var Products = Enumerable.Range(1, 100)
        .Select(i=> new Product
        {
            Id  = i,
            Name = $"Продукт {i}",
            Code = i+10
        }).ToArray();

        Items = Products.AsQueryable();
    }
    
   

    public IQueryable<Product> Items { get; }

    public Product Add(Product item)
    {
        throw new System.NotImplementedException();
    }

    public Task<Product> AddAsync(Product item, CancellationToken Cancel = default)
    {
        throw new System.NotImplementedException();
    }

    public Product Get(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<Product> GetAsync(int id, CancellationToken Cancel = default)
    {
        throw new System.NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task RemoveAsync(int id, CancellationToken Cancel = default)
    {
        throw new System.NotImplementedException();
    }

    public void Update(Product item)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateAsync(Product item, CancellationToken Cancel = default)
    {
        throw new System.NotImplementedException();
    }
}