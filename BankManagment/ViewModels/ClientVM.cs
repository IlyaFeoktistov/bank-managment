using BankManagment.Models.Clients;
using BankManagment.Infrastructure.LoginManager;
using BankManagment.Models.Users;
using BankManagment.Models.Users.Abstract;
using BankManagment.Infrastructure.Command.Abstract;
using BankManagment.ViewModels.Abstract;
using BankManagment.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BankManagment.Infrastructure.Repositories;
using AppServicesLibrary.Services.Messenger;
using AppServicesLibrary.Services.Attributes.FiltredGetingProperties;

namespace BankManagment.ViewModels
{
    internal class ClientVM : ViewModel
    {
        private Client? selectedClient;

        public User User 
        {
            get => LoginManager.Default.CurrentUser ?? new Consultant();
        }

        public ClientVM()
        {
            Repository.ClientRepository.ResetedId += (s, e) => { OnPropertyChanged("Id"); };
            
            AddClientCommand = new RelayCommand(OnAddClientCommandExecute, CanAddClientCommandExecuted);
            RemoveClientCommand = new RelayCommand(OnRemoveClientCommandExecute, CanRemoveClientCommandExecuted);
            EditClientCommand = new RelayCommand(OnEditClientCommandExecute, CanEditClientCommandExecuted);
            ShowClientRecordsCommand = new RelayCommand(OnShowClientRecordsCommandExecute, CanShowClientInfoCommandExecuted);
            ShowClientAccountsCommand = new RelayCommand(OnShowClientAccountsCommandExecute, CanShowClientInfoCommandExecuted);
        }

        public override string ViewModelName => "Client";

        public ObservableCollection<Client> Clients => Repository.ClientRepository.GetClients();
        public Client? SelectedClient 
        {
            get => selectedClient;
            set => SetProperty(ref selectedClient, value);
        }

        public ICommand AddClientCommand { get; }
        public ICommand RemoveClientCommand { get; }
        public ICommand EditClientCommand { get; }
        public ICommand ShowClientRecordsCommand { get; }
        public ICommand ShowClientAccountsCommand { get; }

        private bool CanEditClientCommandExecuted(object? arg) => arg is Client;

        private void OnEditClientCommandExecute(object? obj)
        {
            ClientWindow clientWindow = new()
            {
                DataContext = new ClientWindowVM((obj as Client)!)
            };

            Messenger.Default.Register(this, delegate (Client[] clientsToSwap)
            {
                AddClientRecords(clientsToSwap[0], ref clientsToSwap[1]);

                Repository.ClientRepository.UpdateClient(clientsToSwap[0], clientsToSwap[1]);

                clientWindow.Close();
            }, "ModifyClient");

            clientWindow.Closed += (s, e) =>
            {
                Messenger.Default.UnRegister(this, "ModifyClient");
            };

            clientWindow.ShowDialog();
        }

        private bool CanRemoveClientCommandExecuted(object? arg) //Remove
        {
            return (arg is Client client) 
                && (Repository.ClientRepository.GetCount() > 0) 
                && Repository.ClientRepository.Contains(client)
                && User.GetType().Equals(typeof(Manager));
        }

        private void OnRemoveClientCommandExecute(object? obj)
        {
            if (obj is Client client)
                Repository.ClientRepository.RemoveClient(client);
        }

        private bool CanAddClientCommandExecuted(object? arg)
        {
            return User.GetType().Equals(typeof(Manager));
        }

        private void OnAddClientCommandExecute(object? obj)
        {
            ClientWindow clientWindow = new()
            {
                DataContext = new ClientWindowVM(Repository.ClientRepository.GetCount())
            };

            Messenger.Default.Register(this, delegate (Client client)
            {
                Repository.ClientRepository.AddClient(client);

                clientWindow.Close();
            }, "AddClient");

            clientWindow.Closed += (s, e) =>
            {
                Messenger.Default.UnRegister(this, "AddClient");
            };

            clientWindow.ShowDialog();
        }

        private void OnShowClientRecordsCommandExecute(object? obj)
        {
            ClientRecordsWindow clientRecordsWindow = new()
            {
                DataContext = new ClientRecordsWindowVM((obj as Client)!)
            };

            clientRecordsWindow.ShowDialog();
        }

        private bool CanShowClientInfoCommandExecuted(object? arg) => arg is Client;

        private void OnShowClientAccountsCommandExecute(object? obj)
        {
            ClientAccountsWindow clientAccountsWindow = new()
            {
                DataContext = new ClientAccountsWindowVM((obj as Client)!)
            };

            clientAccountsWindow.Show();
        }

        private void AddClientRecords(Client oldClient, ref Client newClient)
        {
            var properties = typeof(Client).GetFilteredProperties();

            foreach (var property in properties)
            {
                string oldClientValue = (oldClient
                    .GetType()
                    .GetProperty(property.Name)?
                    .GetValue(oldClient) as string) ?? String.Empty;

                string newClientValue = (newClient
                    .GetType()
                    .GetProperty(property.Name)?
                    .GetValue(newClient) as string)!;

                if(oldClientValue != newClientValue)
                {
                    RecordTypes recordType = Record.GetRecordType(oldClientValue, newClientValue);
                    newClient.AddRecord(DateTime.Now, User.Name, recordType, property.Name);
                }
            }
        }
    }
}
