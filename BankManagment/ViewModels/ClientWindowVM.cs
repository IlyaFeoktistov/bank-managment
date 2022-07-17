using BankManagment.Models.Clients;
using BankManagment.Infrastructure.LoginManager;
using BankManagment.Models.Users;
using BankManagment.Models.Users.Abstract;
using BankManagment.Infrastructure.Command.Abstract;
using BankManagment.ViewModels.Abstract;
using System;
using System.Windows.Input;
using AppServicesLibrary.Services.Messenger;

namespace BankManagment.ViewModels
{
    internal class ClientWindowVM : ViewModel
    {
        private ICommand? saveCommand;
        private readonly Client? currentClient;

        private readonly int id;
        private readonly User? user;

        public ClientWindowVM(int newClientId)
        {
            id = newClientId;
            user = LoginManager.Default.CurrentUser;
        }

        public ClientWindowVM(Client client)
        {
            currentClient = client;
            user = LoginManager.Default.CurrentUser;

            id = currentClient.Id;
            Name = currentClient.Name;
            Surname = currentClient.Surname;
            Patronymic = currentClient.Patronymic;
            Phone = currentClient.Phone;

            if (user!.GetType().Equals(typeof(Manager)))
                Passport = currentClient.Passport;
            else
                Passport = "********";
        }

        public override string ViewModelName => "ClientWindow";

        public string Name { get; set; } = String.Empty;
        public string Surname { get; set; } = String.Empty;
        public string Patronymic { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;
        public string Passport { get; set; } = String.Empty;

        public ICommand SaveCommand 
        { 
            get => saveCommand ??= new RelayCommand(OnSaveCommandExecute, CanSaveCommandExecuted);
        }

        private bool CanSaveCommandExecuted(object? arg)
        {
            if (user!.GetType().Equals(typeof(Manager)))
            {
                return Name != String.Empty
                    && Surname != String.Empty
                    && Patronymic != String.Empty
                    && Phone != String.Empty
                    && Passport != String.Empty;
            }
            else
                return Phone != String.Empty;
        }

        private void OnSaveCommandExecute(object? obj)
        {
            Client? newClient;

            if (currentClient == null)
            {
                newClient = new(id, Name, Surname, Patronymic, Phone, Passport);
                Messenger.Default.Send(newClient, "AddClient");
            }
            else
            {
                if (user!.GetType().Equals(typeof(Manager)))
                {
                    newClient = new(id, 
                        Name, 
                        Surname, 
                        Patronymic, 
                        Phone, 
                        Passport);
                }
                else
                {
                    newClient = new(id, 
                        currentClient.Name, 
                        currentClient.Surname, 
                        currentClient.Patronymic, 
                        Phone, 
                        currentClient.Passport);
                }

                newClient.CopyRecords(currentClient.Records);

                Messenger.Default.Send(new Client[2] { currentClient, newClient }, "ModifyClient");
            }
        }
    }
}
