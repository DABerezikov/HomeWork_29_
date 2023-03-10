using HomeWork_29_DB.Entityes.Base;

namespace HomeWork_29_DB.Entityes;

public class Buyer : Person
{
    public string Email { get; set; }
    public string? Phone { get; set; }

    public override string ToString() => $"Покупатель {Surname} {Name} {Patronymic} {Phone} {Email}";
    
}