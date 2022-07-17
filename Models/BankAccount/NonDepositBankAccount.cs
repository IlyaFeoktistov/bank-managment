using BankManagment.Models.BankAccount.Abstract;
using BankManagment.Models.BankAccount.Currencies.Abstract;
using BankManagment.Models.Clients;

namespace BankManagment.Models.BankAccount
{
    internal class NonDepositBankAccount : BankAccountBase
    {
        public NonDepositBankAccount(Client owner, Currency currency, bool isClosed) : base(owner, currency, "Не депозитный", isClosed)
        {
        }
    }
}
