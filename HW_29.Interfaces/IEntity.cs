namespace HW_29.Interfaces;

public interface IEntity
{
    int Id { get; set; }
}

public interface IRepository<T> where T : class, IEntity, new()
{
    IQueryable<T> Items { get;}
    T Get(int id);
    Task<T> GetAsync(int id, CancellationToken Cancel = default);
    T Add(T item);
    Task<T> GetAsync(T item, CancellationToken Cancel = default);
    void Update(T item);
    Task<T> UpdateAsync(T item, CancellationToken Cancel = default);
    void Remove (int id);
    Task RemoveAsync(int id, CancellationToken Cancel = default);
}