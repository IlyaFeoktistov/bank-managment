using AppServicesLibrary.Services.NotifyPropertyChanged;
using BankManagment.Models.BankAccount.Currencies.Exceptions;

namespace BankManagment.Models.BankAccount.Currencies.Abstract
{
    public class Currency : NotifyPropertyChanged
    {
        private float amount;

        public Currency(string name, int currencyCode, float amount) 
        {
            Name = name;
            CurrencyCode = currencyCode;
            Amount = amount;
        }

        public string Name { get; }
        public int CurrencyCode { get; }
        public float Amount 
        {
            get => amount;
            private set => SetProperty(ref amount, value); 
        }
        
        /// <summary>
        /// Пополняет счет на сумму
        /// </summary>
        /// <param name="amount">Сумма</param>
        public void Credit(float amount)
        {
            Amount += amount;
        }

        /// <summary>
        /// Списывает со счета ДС на сумму
        /// </summary>
        /// <param name="amount">Сумма</param>
        /// <exception cref="EmptyAmountException"></exception>
        public void Debit(float amount)
        {
            if (Amount - amount < 0) 
                throw new EmptyAmountException();
            else
                Amount -= amount;
        }
    }
}
