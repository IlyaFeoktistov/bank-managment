using BankManagment.Models.BankAccount.Currencies.Abstract;

namespace BankManagment.Models.BankAccount.Currencies
{
    public class Dollars : Currency
    {
        public Dollars(float amount = 0) : base("Доллар", 840, amount)
        {
        }
    }
}
