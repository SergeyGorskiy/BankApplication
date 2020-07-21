using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public abstract class Account : IAccount   // Абстрактный класс, реализует интерфейс
    {
        // Определяем события с помощью делегата:
        protected internal event AccountStateHandler Withdrawed;  //Событие, возникающее при выводе денег
        protected internal event AccountStateHandler Added;  // Событие возникающее при добавлении на счет
        protected internal event AccountStateHandler Opened;  // Событие возникающее при открытии счета
        protected internal event AccountStateHandler Closed;  // Событие возникающее при закрытии счета
        protected internal event AccountStateHandler Calculated;  // Событие возникающее при начислении процентов

        static int counter = 0;  // Статический счетчик для получения уникального номера счета Id
        protected int _days = 0;  // время с момента открытия счета

        public Account(decimal sum, int percentage)  // Конструктор для класса
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++counter;
        }

        public decimal Sum { get; private set; }  // Текущая сумма на счету
        public int Percentage { get; private set; }  // Процент начислений
        public int Id { get; private set; }  // Уникальный идентификатор счета
        
        // Определяем методы генерации:
        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)  // Вызов событий
        {
            if (e != null)
            {
                handler?.Invoke(this, e);
            }
        }

        // Вызов отдельных событий. Для каждого события определяется свой виртуальный метод
        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }
        public virtual void Put(decimal sum)  // Метод пополнения счета, возвращает сколько поступило на счет
        {
            Sum += sum;
            OnAdded(new AccountEventArgs("На счет поступило " + sum, sum));  
        }
        public virtual decimal Withdraw(decimal sum)  // Метод снятия со счета, возвращает сколько снято со счета
        {
            decimal result = 0;
            if (Sum >= sum)
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs($"Сумма {sum} снята со счета {Id}", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs($"Недостаточно денег на счете {Id}", 0));
            }
            return result;
        }
        protected internal virtual void Open()  // открытие счета
        {
            OnOpened(new AccountEventArgs($"Открыт новый счет! Id счета: {Id}", Sum));
        }
        protected internal virtual void Close()  // закрытие счета
        {
            OnClosed(new AccountEventArgs($"Счет {Id} закрыт.  Итоговая сумма: {Sum}", Sum));
        }
        protected internal void IncrementDays()  // Переход на новый день
        {
            _days++;
        }
        protected internal virtual void Calculate() // Начисление процентов
        {
            decimal increment = Sum * Percentage / 100;
            Sum = Sum + increment;
            OnCalculated(new AccountEventArgs($"Начислены проценты в размере: {increment}", increment));
        }
    }
}
