﻿namespace HomeWork_29_DB.Entityes.Base;

public abstract class Person : NamedEntity
{
    public string Surname { get; set; }
    public  string Patronymic { get; set; }


}