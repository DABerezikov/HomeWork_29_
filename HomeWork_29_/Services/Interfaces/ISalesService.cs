using HomeWork_29_DB.Entityes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWork_29_.Services.Interfaces;

public interface ISalesService
{
    IEnumerable<Deal> Deals { get; }

    Task<Deal> MakeADeal(string ProductName, Buyer Buyer);
    
}