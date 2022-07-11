using BankManagment.Models.Clients;
using BankManagment.Services.DataReader;
using BankManagment.Services.DataWriter;
using BankManagment.Services.DataWriter.Writers;
using System;
using System.Collections.ObjectModel;

namespace BankManagment.Services.Repositories
{
    internal class ClientRepository
    {
        private readonly ObservableCollection<Client> clients;

        public event EventHandler? ResetedId; 

        public ClientRepository()
        {
            clients = SetUpReposytory();
        }

        public void AddClient(Client client)
        {
            clients.Add(client);

            UpdateRepository();
        }

        public void RemoveClient(Client client)
        {
            clients.Remove(client);

            ResetClientsId();

            UpdateRepository();
        }

        public void UpdateClient(Client oldClient, Client newClient)
        {
            var oldIndex = clients.IndexOf(oldClient);

            //if (oldIndex == -1) return;

            //clients.RemoveAt(oldIndex);
            //clients.Insert(oldIndex, newClient);

            clients.RemoveAt(oldClient.Id);
            clients.Insert(oldClient.Id, newClient);

            UpdateRepository();
        }

        public bool Contains(Client client) => clients.Contains(client);

        public ObservableCollection<Client> GetClients() => clients;

        public int GetCount() => clients.Count;

        private void UpdateRepository()
        {
            WriterManager writer = new(new JSONDataWriter(Constants.ClientRepository.DATA_FILE));
            writer.GetData(clients);
            writer.Write();
        }

        private static ObservableCollection<Client> SetUpReposytory()
        {
            ReaderManager reader = new(new JSONDataReader(Constants.ClientRepository.DATA_FILE));
            ObservableCollection<Client> clients = reader.Read<ObservableCollection<Client>>() ?? new();

            return clients;
        }

        private void ResetClientsId()
        {
            for (int id = 0; id < clients.Count; id++)
            {
                clients[id].Id = id;
            }

            ResetedId?.Invoke(this, new EventArgs());
        }
    }
}
