using HomeWork_29_DB.Context;
using HomeWork_29_DB.Entityes.Base;
using HW_29.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeWork_29_DB;

public class DbRepository<T> : IRepository<T> where T : Entity, new()
{
    private readonly HW_29_DB _db;
    private readonly DbSet<T> _Set;
    public DbRepository(HW_29_DB db)
    {
        _db = db;
        _Set = _db.Set<T>();
    } 
    
    
    public virtual IQueryable<T> Items => _Set;

    public T Add(T item)
    {
        throw new NotImplementedException();
    }

    public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);

    public async Task<T> GetAsync(int id, CancellationToken Cancel = default) => await Items
        .SingleOrDefaultAsync(item => item.Id == id, Cancel)
        .ConfigureAwait(false);

    public Task<T> GetAsync(T item, CancellationToken Cancel = default)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id, CancellationToken Cancel = default)
    {
        throw new NotImplementedException();
    }

    public void Update(T item)
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateAsync(T item, CancellationToken Cancel = default)
    {
        throw new NotImplementedException();
    }
}