using BankManagment.Models.BankAccount.Abstract;
using BankManagment.Models.BankAccount.Currencies.Abstract;
using BankManagment.Models.Clients;

namespace BankManagment.Models.BankAccount
{
    internal class NonDepositBankAccount : BankAccountBase
    {
        public NonDepositBankAccount(int ownerId, Currency currency, bool isClosed) : base(ownerId, currency, "Не депозитный", isClosed)
        {
        }
    }
}
