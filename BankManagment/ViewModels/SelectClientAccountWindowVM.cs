using BankManagment.Models.BankAccount.Abstract;
using BankManagment.Models.Clients;
using BankManagment.Infrastructure.Command.Abstract;
using AppServicesLibrary.Services.Messenger;
using BankManagment.Infrastructure.Repositories;
using BankManagment.ViewModels.Abstract;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BankManagment.ViewModels
{
    internal class SelectClientAccountWindowVM : ViewModel
    {
        private ICommand? selectAccount;

        private Client? selectedClient;

        public SelectClientAccountWindowVM() { }

        public override string ViewModelName => "SelectClientAccountWindow";

        public ObservableCollection<Client> Clients => 
            Repository.ClientRepository.GetClients();

        public Client? SelectedClient 
        {
            get => selectedClient;
            set => SetProperty(ref selectedClient, value);
        }
        public BankAccountBase? SelectedAccount { get; set; }

        public ICommand SelectAccount => 
            selectAccount ??= new RelayCommand(PerformSelectAccount);

        private void PerformSelectAccount(object? obj)
        {
            Messenger.Default.Send(SelectedAccount, "GetAccount");
            RequestClose?.Invoke();
        }
    }
}
