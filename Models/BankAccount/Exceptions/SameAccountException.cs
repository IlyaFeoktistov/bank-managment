using System;

namespace BankManagment.Models.BankAccount.Exceptions
{
    public class SameAccountException : Exception
    {
        private const string MESSAGE = "Невозможно отправить средства на тот же самый счет";
        public SameAccountException() :base(MESSAGE) { }
    }
}
