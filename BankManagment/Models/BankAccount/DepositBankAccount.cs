using BankManagment.Models.BankAccount.Abstract;
using BankManagment.Models.BankAccount.Currencies.Abstract;

namespace BankManagment.Models.BankAccount
{
    internal class DepositBankAccount : BankAccountBase
    {
        public DepositBankAccount(int ownerId, Currency currency, bool isClosed) : base(ownerId, currency, "Депозитный", isClosed)
        {
        }
    }
}
