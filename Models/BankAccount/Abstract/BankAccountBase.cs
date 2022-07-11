using BankManagment.Models.BankAccount.Currencies.Abstract;
using BankManagment.Models.BankAccount.Currencies.Exceptions;
using BankManagment.Models.BankAccount.Exceptions;
using BankManagment.Models.Clients;
using BankManagment.Services.NotifyPropertyChanged;

namespace BankManagment.Models.BankAccount.Abstract
{
    public class BankAccountBase : NotifyPropertyChanged, IBankAccount
    {
        private bool isClosed;

        public BankAccountBase(Client owner, Currency currency, string name)
        {
            Owner = owner;
            Currency = currency;
            Name = name;

            Currency.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };

            OpenAccount();
        }

        public string Name { get; }

        public Client Owner { get; }
        public Currency Currency { get; }
        public bool IsClosed 
        {
            get => isClosed;
            private set => SetProperty(ref isClosed, value); 
        }

        public void OpenAccount() => IsClosed = false;

        public void CloseAccount() => IsClosed = true;

        /// <summary>
        /// Получает ДС на счет
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromAccount">С какого счета</param>
        /// <param name="amount">Сумма</param>
        /// <exception cref="IsClosedException"></exception>
        public void GetMoney<T>(T fromAccount, decimal amount) where T : BankAccountBase
        {
            if (IsClosed) throw new IsClosedException();
            
            Currency.Credit(amount);
        }

        /// <summary>
        /// Отправляет ДС на счет
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toAccount">На какой счет</param>
        /// <param name="amount">Сумма</param>
        /// <exception cref="EmptyAmountException"></exception>
        public void SendMoney<T>(T toAccount, decimal amount) where T : BankAccountBase
        {
            if (IsClosed) throw new IsClosedException();
            if (toAccount is null || toAccount.Equals(this)) return;
            
            try
            {
                Currency.Debit(amount);
            }
            catch(EmptyAmountException)
            {
                throw new EmptyAmountException();
            }

            toAccount.GetMoney(this, amount);
        }

        public void Deposit(decimal amount)
        {
            if (IsClosed) throw new IsClosedException();

            Currency.Credit(amount);
        }
    }
}
