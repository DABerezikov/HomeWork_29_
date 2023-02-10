using HomeWork_29_DB.Context;
using HomeWork_29_DB.Entityes;
using HomeWork_29_DB.Entityes.Base;
using HW_29.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_29_DB;

internal class DbRepository<T> : IRepository<T> where T : Entity, new()
{
    private readonly HW_29_DB _db;
    private readonly DbSet<T> _Set;
    public bool AutoSaveChanges { get; set; } = true;
    public DbRepository(HW_29_DB db)
    {
        _db = db;
        _Set = _db.Set<T>();
    }


    public virtual IQueryable<T> Items => _Set;

    public T Add(T item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        _db.Entry(item).State = EntityState.Added;
        if (AutoSaveChanges)
            _db.SaveChanges();
        return item;
    }

    public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);

    public async Task<T> GetAsync(int id, CancellationToken Cancel = default) => await Items
        .SingleOrDefaultAsync(item => item.Id == id, Cancel)
        .ConfigureAwait(false);

    public async Task<T> AddAsync(T item, CancellationToken Cancel = default)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        _db.Entry(item).State = EntityState.Added;
        if (AutoSaveChanges)
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        return item;
    }

    public void Remove(int id)
    {
        //var item = Get(id);
        //if (item is null) return;
        //_db.Entry(item);
        _db.Remove(new T { Id = id });
        if (AutoSaveChanges)
            _db.SaveChanges();
    }

    public async Task RemoveAsync(int id, CancellationToken Cancel = default)
    {
        _db.Remove(new T { Id = id });
        if (AutoSaveChanges)
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
    }

    public void Update(T item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        _db.Entry(item).State = EntityState.Modified;
        if (AutoSaveChanges)
            _db.SaveChanges();


    }

    public async Task UpdateAsync(T item, CancellationToken Cancel = default)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        _db.Entry(item).State = EntityState.Modified;
        if (AutoSaveChanges)
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
    }

   
} 
class DealsRepository : DbRepository<Deal>
    {
        public override IQueryable<Deal> Items => base.Items
            .Include(item => item.Buyer)
            .Include(item=>item.Products)
        ;
        public DealsRepository(HW_29_DB db) : base(db) { }
    }