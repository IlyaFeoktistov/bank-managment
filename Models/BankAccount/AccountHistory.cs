using System;

namespace BankManagment.Models.BankAccount
{
    public class AccountHistory
    {
        public AccountHistory(int id, DateTime time, AccountAction accountAction, string details)
        {
            Id = id;
            Time = time;
            AccountAction = accountAction;
            Details = details;
        }

        public int Id { get; }
        public DateTime Time { get; }
        public AccountAction AccountAction { get; }
        public string Details { get; private set; }
    }
}
