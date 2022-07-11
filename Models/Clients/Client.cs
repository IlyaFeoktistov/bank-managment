using BankManagment.Models.BankAccount.Abstract;
using BankManagment.Services.Attributes.FiltredGetingProperties;
using BankManagment.Services.NotifyPropertyChanged;
using System;
using System.Collections.ObjectModel;

namespace BankManagment.Models.Clients
{
    public class Client : NotifyPropertyChanged
    {
        private int id;

        public Client(int id, string name, string surname, string patronymic, string phone, string passport)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Phone = phone;
            Passport = passport;
            Records = new();
            BankAccounts = new();
        }

        [SkipProperty]
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Passport { get; set; }

        [SkipProperty]
        public ObservableCollection<Record> Records { get; }

        [SkipProperty]
        public ObservableCollection<BankAccountBase> BankAccounts { get; }

        public static Client CreateNewClient(int newClientId)
        {
            return new Client(newClientId,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty);
        }

        #region Methods Records

        public void CopyRecords(ObservableCollection<Record> records)
        {
            Records.Clear();

            foreach (var record in records)
            {
                Records.Add(record);
            }
        }
        public void AddRecord(DateTime now, string user, RecordTypes recordType, string propertyName)
        {
            Records.Add(new Record(Records.Count, now, user, recordType, propertyName));
        }

        public void ClearRecords() => Records.Clear();

        #endregion

        #region Methods BankAccounts

        public void AddBankAccount(BankAccountBase account)
        {
            BankAccounts.Add(account);
        }

        public void RemoveBankAccount(BankAccountBase account)
        {
            BankAccounts.Remove(account);
        }

        public void CopyBankAccounts(ObservableCollection<BankAccountBase> bankAccounts)
        {
            BankAccounts.Clear();

            foreach (var account in bankAccounts)
            {
                BankAccounts.Add(account);
            }
        }

        #endregion

        public Client Copy()
        {
            Client client = new Client(Id, Name, Surname, Patronymic, Phone, Passport);
            client.CopyRecords(Records);
            client.CopyBankAccounts(BankAccounts);

            return client;
        }
    }
}
