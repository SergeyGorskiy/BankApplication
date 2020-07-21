using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class DepositAccount : Account  // Класс представляет Депозитный счет.
    {
        public DepositAccount(decimal sum, int percentage) : base(sum, percentage)  // Конструктор класса
        { 
        }
        protected internal override void Open()  // Переопределяем метод Открытие счета
        {
            base.OnOpened(new AccountEventArgs($"Открыт новый депозитный счет! Id счета: {this.Id}", this.Sum));
        }
        public override void Put(decimal sum)  // Переопределяем метод Пополнение счета
        {
            if (_days % 30 == 0)
                base.Put(sum);
            else
                base.OnAdded(new AccountEventArgs("На счет можно положить только после 30-ти дневного периода", 0));
        }
        public override decimal Withdraw(decimal sum)  // Переопределяем метод Снятие со счета
        {
            if (_days % 30 == 0)
                return base.Withdraw(sum);
            else
                base.OnWithdrawed(new AccountEventArgs("Вывести средства можно только после 30-ти дневного периода", 0));
            return 0;
        }
        protected internal override void Calculate()  // Переопределяем метод Начисление процентов
        {
            if (_days % 30 == 0)
                base.Calculate();
        }
    }
}
