using BankManagment.Models.BankAccount;
using BankManagment.Models.BankAccount.Abstract;
using BankManagment.Models.BankAccount.Currencies;
using BankManagment.Models.BankAccount.Currencies.Abstract;
using BankManagment.Services.Command.Abstract;
using BankManagment.Services.Messenger;
using BankManagment.ViewModels.Abstract;
using System;
using System.Linq;
using System.Windows.Input;

namespace BankManagment.ViewModels
{
    internal class SelectCurrencyAccountWindowVM : ViewModel
    {
        private ICommand? selectCurrency;

        public SelectCurrencyAccountWindowVM()
        {
            Currencies = new Currency[]
            {
                new Rubles(),
                new Dollars()
            };

            SelectedCurrency = Currencies.FirstOrDefault()!;
        }

        public override string ViewModelName => "SelectCurrencyAccountWindow";

        public Currency[] Currencies { get; }
        public Currency SelectedCurrency { get; set; }

        public bool IsDeposit { get; set; }

        public ICommand SelectCurrency 
        { 
            get => selectCurrency ??= new RelayCommand(PerformSelectCurrency); 
        }

        private void PerformSelectCurrency(object? obj)
        {
            var result = (currency:SelectedCurrency, isDeposit: IsDeposit);
            Messenger.Default.Send(result, "GetCurrency");
            RequestClose?.Invoke();
        }
    }
}
