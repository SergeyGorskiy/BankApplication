using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class DemandAccount : Account  // Класс представляет счет "До востребования".
    {
        public DemandAccount(decimal sum, int percentage) : base(sum, percentage)  // Конструктор класса
        {

        }
        protected internal override void Open()  // Переопределяем метод Открытие счета
        {
            base.OnOpened(new AccountEventArgs($"Открыт новый счет до востребования! Id счета: {this.Id}", this.Sum));
        }
    }
}
