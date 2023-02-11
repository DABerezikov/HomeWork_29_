using HomeWork_29_DB.Entityes.Base;
using System.Numerics;

namespace HomeWork_29_DB.Entityes;

public class Deal : Entity
{
    public virtual Product Products { get; set; }
    public virtual Buyer Buyer { get; set; }

    public override string ToString() => $"Покупатель {Buyer.Surname} {Buyer.Name} {Buyer.Email} купил {Products.Name}";
}