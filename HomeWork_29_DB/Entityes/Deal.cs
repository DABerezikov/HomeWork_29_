using HomeWork_29_DB.Entityes.Base;

namespace HomeWork_29_DB.Entityes;

public class Deal : Entity
{
    public virtual Product Product { get; set; }
    public virtual Buyer Buyer { get; set; }
}