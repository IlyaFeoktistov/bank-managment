using BankManagment.Models.BankAccount;
using BankManagment.Models.BankAccount.Abstract;
using BankManagment.Models.BankAccount.Currencies.Abstract;
using BankManagment.Models.Clients;
using BankManagment.Services.Command.Abstract;
using BankManagment.Services.Messenger;
using BankManagment.Services.Repositories;
using BankManagment.ViewModels.Abstract;
using BankManagment.Views;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace BankManagment.ViewModels
{
    internal class ClientAccountsWindowVM : ViewModel
    {
        private decimal amount;
        private decimal amountDeposit;
        private decimal amountToSend;

        private Client client;
        private Client? selectedClient;

        private ICommand? openAccountCommand;
        private ICommand? closeAccountCommand;
        private ICommand? sendMoneyCommand;
        private ICommand? selectClientAccount;
        private ICommand? depositCommand;

        private BankAccountBase? selectedFromAccount;
        private BankAccountBase? selectedToAccount;

        public ClientAccountsWindowVM(Client client)
        {
            this.client = client;
        }

        public override string ViewModelName => "ClientAccountsWindow";

        
        public ICommand OpenAccountCommand => openAccountCommand ??= new RelayCommand(OpenAccount);
        public ICommand CloseAccountCommand => closeAccountCommand ??= new RelayCommand(CloseAccount, CanCloseAccount);
        public ICommand SelectClientAccount => selectClientAccount ??= new RelayCommand(PerformSelectClientAccount);
        public ICommand SendMoneyCommand => sendMoneyCommand ??= new RelayCommand(SendMoney);
        public ICommand DepositCommand => depositCommand ??= new RelayCommand(Deposit, CanDeposit);

        public decimal Amount 
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public decimal AmountDeposit 
        {
            get => amountDeposit;
            set => SetProperty(ref amountDeposit, value);
        }

        public decimal AmountToSend
        {
            get => amountToSend;
            set => SetProperty(ref amountToSend, value);
        }

        public ObservableCollection<BankAccountBase> Accounts 
        { 
            get => client.BankAccounts;
        }

        public BankAccountBase? SelectedFromAccount 
        { 
            get => selectedFromAccount;
            set
            {
                SetProperty(ref selectedFromAccount, value);
                if(selectedFromAccount != null)
                    selectedFromAccount.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };
            } 
        }
        public BankAccountBase? SelectedToAccount 
        { 
            get => selectedToAccount; 
            set
            {
                SetProperty(ref selectedToAccount, value);
                if (selectedToAccount != null)
                    selectedToAccount.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };
            }
        }

        public Client? SelectedClient 
        { 
            get => selectedClient; 
            set => SetProperty(ref selectedClient, value); 
        }

        private void PerformSelectClientAccount(object? commandParameter)
        {
            SelectClientAccountWindowVM vm = new();
            SelectClientAccountWindow selectAccountClientWindow = new();

            vm.RequestClose += () => 
            { 
                selectAccountClientWindow.Close(); 
            };

            selectAccountClientWindow.DataContext = vm;

            Messenger.Default.Register(this,
                delegate (BankAccountBase account)
                {
                    SelectedToAccount = account;
                },
                "GetAccount");

            selectAccountClientWindow.Closed += (s, e) =>
            {
                Messenger.Default.UnRegister(this, "GetAccount");
            };

            selectAccountClientWindow.ShowDialog();
        }

        private bool CanCloseAccount(object? account) => account is BankAccountBase;

        private void CloseAccount(object? account)
        {
            var oldClient = client.Copy();

            selectedFromAccount?.CloseAccount();

            Repository.ClientRepository.UpdateClient(oldClient, client);
        }

        private void OpenAccount(object? commandParameter)
        {
            SelectCurrencyAccountWindowVM vm = new();
            SelectCurrencyAccountWindow selectCurrencyAccountWindow = new();

            vm.RequestClose += () => 
            { 
                selectCurrencyAccountWindow.Close(); 
            };

            selectCurrencyAccountWindow.DataContext = vm;

            Messenger.Default.Register(this, 
                delegate ((Currency currency, bool isDeposit) result) 
                {
                    var oldClient = client.Copy();

                    foreach(var clientAccount in client.BankAccounts)
                    {
                        if(result.isDeposit)
                        {
                            if (!clientAccount.IsClosed && clientAccount.Name == "Депозитный")
                            {
                                MessageBox.Show("Депозитный счет уже существует!");
                                return;
                            }
                        }
                        else
                        {
                            if (!clientAccount.IsClosed && clientAccount.Name == "Не депозитный")
                            {
                                MessageBox.Show("Не депозитный счет уже существует!");
                                return;
                            }
                        }
                    }

                    BankAccountBase account = result.isDeposit switch
                    {
                        true => new DepositBankAccount(client, result.currency),
                        false => new NonDepositBankAccount(client, result.currency)
                    };

                    client.AddBankAccount(account);

                    Repository.ClientRepository.UpdateClient(oldClient, client);
                }, 
                "GetCurrency");

            selectCurrencyAccountWindow.Closed += (s, e) => 
            { 
                Messenger.Default.UnRegister(this, "GetCurrency"); 
            };

            selectCurrencyAccountWindow.ShowDialog();
        }

        private void SendMoney(object? commandParameter)
        {
            var oldClient = client.Copy();
            
            try
            {
                selectedFromAccount?.SendMoney(selectedToAccount!, amountToSend);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            Repository.ClientRepository.UpdateClient(oldClient, client);
        }

        private bool CanDeposit(object? obj) => obj is not null;
        private void Deposit(object? obj)
        {
            var oldClient = client.Copy();
            try
            {
                decimal amount = 0;

                if(obj != null)
                    _ = decimal.TryParse(obj.ToString(), out amount);

                selectedFromAccount?.Deposit(amount);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            Repository.ClientRepository.UpdateClient(oldClient, client);
        }
    }
}
