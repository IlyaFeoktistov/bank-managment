namespace BankManagment.Models.BankAccount
{
    public class AccountAction
    {
        public AccountAction(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static AccountAction Transfer { get => new("Перевод"); }
        public static AccountAction Deposit { get => new("Пополнение"); }
        public static AccountAction OpenAccount { get => new("Открытие счета"); }
        public static AccountAction CloseAccount { get => new("Закрытие счета"); }
    }
}
