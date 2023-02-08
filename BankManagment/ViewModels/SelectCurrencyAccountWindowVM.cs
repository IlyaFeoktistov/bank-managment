using BankManagment.Models.BankAccount.Currencies;
using BankManagment.Models.BankAccount.Currencies.Abstract;
using BankManagment.Infrastructure.Command.Abstract;
using AppServicesLibrary.Services.Messenger;
using BankManagment.ViewModels.Abstract;
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

        public ICommand SelectCurrency => 
            selectCurrency ??= new RelayCommand(PerformSelectCurrency);

        private void PerformSelectCurrency(object? obj)
        {
            var result = (currency: SelectedCurrency, isDeposit: IsDeposit);
            Messenger.Default.Send(result, "GetCurrency");
            RequestClose?.Invoke();
        }
    }
}
