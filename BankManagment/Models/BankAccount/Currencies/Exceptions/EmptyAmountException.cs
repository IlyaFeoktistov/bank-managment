using System;

namespace BankManagment.Models.BankAccount.Currencies.Exceptions
{
    public class EmptyAmountException : Exception
    {
        private const string MESSAGE = "Недостаточно денег";

        public EmptyAmountException() : base(MESSAGE)
        {

        }
    }
}
