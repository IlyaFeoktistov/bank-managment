using BankManagment.Models.BankAccount.Currencies.Exceptions;
using BankManagment.Services.NotifyPropertyChanged;

namespace BankManagment.Models.BankAccount.Currencies.Abstract
{
    public class Currency : NotifyPropertyChanged
    {
        private decimal amount;

        public Currency(string name, int currencyCode, decimal amount) 
        {
            Name = name;
            CurrencyCode = currencyCode;
            Amount = amount;
        }

        public string Name { get; }
        public int CurrencyCode { get; }
        public decimal Amount 
        {
            get => amount;
            private set => SetProperty(ref amount, value); 
        }
        
        /// <summary>
        /// Пополняет счет на сумму
        /// </summary>
        /// <param name="amount">Сумма</param>
        public void Credit(decimal amount)
        {
            Amount += amount;
        }

        /// <summary>
        /// Списывает со счета ДС на сумму
        /// </summary>
        /// <param name="amount">Сумма</param>
        /// <exception cref="EmptyAmountException"></exception>
        public void Debit(decimal amount)
        {
            if (Amount - amount < 0) 
                throw new EmptyAmountException();
            else
                Amount -= amount;
        }
    }
}
