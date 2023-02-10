using HomeWork_29_DB.Context;
using HomeWork_29_DB.Entityes;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_29_DB;

class DealsRepository : DbRepository<Deal>
{
    public override IQueryable<Deal> Items => base.Items
        .Include(item => item.Buyer)
        .Include(item=>item.Product)
    ;
    public DealsRepository(HW_29_DB db) : base(db) { }
}