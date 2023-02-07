using HomeWork_29_DB.Entityes.Base;

namespace HomeWork_29_DB.Entityes;

public class Buyer : Person
{
    public string Email { get; set; }
    public virtual ICollection<Product> Products { get; set; }
    
}