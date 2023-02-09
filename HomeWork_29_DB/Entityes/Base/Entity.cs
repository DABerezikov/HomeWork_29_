using HW_29.Interfaces;

namespace HomeWork_29_DB.Entityes.Base;

public abstract class Entity : IEntity
{
    public int Id { get; set; }
}