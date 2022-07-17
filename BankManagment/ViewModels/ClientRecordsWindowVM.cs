using BankManagment.Models.Clients;
using System.Collections.ObjectModel;

namespace BankManagment.ViewModels
{
    internal class ClientRecordsWindowVM
    {
        private readonly Client client;

        public ClientRecordsWindowVM(Client client)
        {
            this.client = client;
        }

        public ObservableCollection<Record> Records
        { 
            get => client.Records; 
        }
    }
}
