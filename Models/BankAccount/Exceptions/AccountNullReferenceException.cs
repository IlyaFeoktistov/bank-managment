using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagment.Models.BankAccount.Exceptions
{
    public class AccountNullReferenceException : Exception
    {
        private const string MESSAGE = "Счет не выбран";
        public AccountNullReferenceException() : base(MESSAGE)
        {
        }
    }
}
