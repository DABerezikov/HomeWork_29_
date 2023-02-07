using HomeWork_29_DB.Entityes.Base;

namespace HomeWork_29_DB.Entityes;

public class Product : NamedEntity
{
    public int Code { get; set; }
   
   
}

public class Buyer : Person
{
    public string Email { get; set; }
    public virtual ICollection<Product> Products { get; set; }
    
}