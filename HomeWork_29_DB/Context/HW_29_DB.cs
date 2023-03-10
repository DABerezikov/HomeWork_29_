using HomeWork_29_DB.Entityes;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_29_DB.Context
{
    public class HW_29_DB : DbContext
    {

       public DbSet<Product> Products { get; set; }
       public  DbSet<Buyer> Buyers { get; set; }
       public DbSet<Deal> Deals { get; set; }


        public HW_29_DB(DbContextOptions<HW_29_DB> options) : base(options)
        {

        }
    }
}
