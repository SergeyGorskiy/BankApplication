using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    // Описывает функциональность банковского счета.
    public interface IAccount
    {
        // Положить деньги на счет
        void Put(decimal sum);

        // Снять деньги со счета
        decimal Withdraw(decimal sum);
    }
}
