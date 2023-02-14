using HomeWork_29_DB.Entityes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_29_.Services.Interfaces
{
    public interface IUserDialog
    {
        bool Edit(Product product);

        bool ConfirmInformation(string Information, string Caption);
        bool ConfirmWarning(string Warning, string Caption);
        bool ConfirmError(string Error, string Caption);
    }
}
