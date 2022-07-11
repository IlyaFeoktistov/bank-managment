using System;

namespace BankManagment.Models.BankAccount.Exceptions
{
    internal class IsClosedException : Exception
    {
        private const string MESSAGE = "Счет закрыт";
        public IsClosedException() : base(MESSAGE)
        {
        }
    }
}
