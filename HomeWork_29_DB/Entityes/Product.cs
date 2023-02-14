using HomeWork_29_DB.Entityes.Base;
using System.Numerics;

namespace HomeWork_29_DB.Entityes;

public class Product : NamedEntity
{
    public int Code { get; set; }

    public override string ToString() => $"Продукт {Id} {Name}";
}