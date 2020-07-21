using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    // Изменения счета обрабатываются через события (событийная модель)
    // Делегат используется для создания событий
    public delegate void AccountStateHandler(object sender, AccountEventArgs e);

    // Обработка событий, определяет 2 свойства для чтения:
    public class AccountEventArgs
    {
        //Сообщение о событии
        public string Message { get; private set; }
       
        // Сумма, на которую изменился счет
        public decimal Sum { get; private set; }
       
        // Конструктор для класса
        public AccountEventArgs(string _mes, decimal _sum)
        {
            Message = _mes;
            Sum = _sum;
        }
    }
}
