using BankManagment.Models.BankAccount.Abstract;
using BankManagment.Models.BankAccount.Currencies.Abstract;
using BankManagment.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagment.Models.BankAccount
{
    internal class DepositBankAccount : BankAccountBase
    {
        public DepositBankAccount(int ownerId, Currency currency, bool isClosed) : base(ownerId, currency, "Депозитный", isClosed)
        {
        }
    }
}
