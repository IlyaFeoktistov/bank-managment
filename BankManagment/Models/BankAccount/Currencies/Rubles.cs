using BankManagment.Models.BankAccount.Currencies.Abstract;

namespace BankManagment.Models.BankAccount.Currencies
{
    public class Rubles : Currency
    {
        public Rubles(float amount = 0) : base("Рубль", 810, amount)
        {
        }
    }
}
