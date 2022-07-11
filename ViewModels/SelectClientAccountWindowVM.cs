using BankManagment.Models.BankAccount.Abstract;
using BankManagment.Models.Clients;
using BankManagment.Services.Command.Abstract;
using BankManagment.Services.Messenger;
using BankManagment.Services.Repositories;
using BankManagment.ViewModels.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BankManagment.ViewModels
{
    internal class SelectClientAccountWindowVM : ViewModel
    {
        private ICommand? selectAccount;

        private Client? selectedClient;
        public SelectClientAccountWindowVM()
        {
            
        }

        public override string ViewModelName => "SelectClientAccountWindow";

        public ObservableCollection<Client> Clients { get => Repository.ClientRepository.GetClients(); }
        public Client? SelectedClient 
        {
            get => selectedClient;
            set => SetProperty(ref selectedClient, value);
        }
        public BankAccountBase? SelectedAccount { get; set; }

        public ICommand SelectAccount { get => selectAccount ??= new RelayCommand(PerformSelectAccount); }

        private void PerformSelectAccount(object? obj)
        {
            Messenger.Default.Send(SelectedAccount, "GetAccount");
            RequestClose?.Invoke();
        }
    }
}
