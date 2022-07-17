using AppServicesLibrary.Services.NotifyPropertyChanged;
using BankManagment.Models.BankAccount.Currencies.Abstract;
using BankManagment.Models.BankAccount.Currencies.Exceptions;
using BankManagment.Models.BankAccount.Exceptions;
using System;
using System.Collections.ObjectModel;

namespace BankManagment.Models.BankAccount.Abstract
{
    public class BankAccountBase : NotifyPropertyChanged, IBankAccount
    {
        private bool isClosed;

        public BankAccountBase(int ownerId, Currency currency, string name, bool isClosed)
        {
            OwnerId = ownerId;
            Currency = currency;
            Name = name;

            this.isClosed = isClosed;

            History = new ObservableCollection<AccountHistory>();

            Currency.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };
        }

        public string Name { get; }

        public int OwnerId { get; }
        public Currency Currency { get; }
        public bool IsClosed 
        {
            get => isClosed;
            private set => SetProperty(ref isClosed, value); 
        }

        public ObservableCollection<AccountHistory> History { get; }

        public void OpenAccount() => IsClosed = false;

        public void CloseAccount() => IsClosed = true;

        /// <summary>
        /// Получает ДС на счет
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromAccount">С какого счета</param>
        /// <param name="amount">Сумма</param>
        /// <exception cref="IsClosedException"></exception>
        public void GetMoney<T>(T fromAccount, float amount) where T : BankAccountBase
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
        /// <exception cref="IsClosedException"></exception>
        /// <exception cref="SameAccountException"></exception>
        /// <exception cref="AccountNullReferenceException"></exception>
        /// <exception cref="EmptyAmountException"></exception>
        public void SendMoney<T>(T toAccount, float amount) where T : BankAccountBase
        {
            if (IsClosed) throw new IsClosedException();
            if (Equals(toAccount, this)) throw new SameAccountException();
            if (toAccount is null) throw new AccountNullReferenceException();
            
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

        public void Deposit(float amount)
        {
            if (IsClosed) throw new IsClosedException();

            Currency.Credit(amount);
        }

        public void AddHistoryRecord(AccountAction action, string details = "")
        {
            History.Add(new AccountHistory(History.Count, DateTime.Now, action, details));
        }
    }
}
