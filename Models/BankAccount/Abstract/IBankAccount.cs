namespace BankManagment.Models.BankAccount.Abstract
{
    public interface IBankAccount
    {
        void SendMoney<T>(T toAccount, decimal amount) where T : BankAccountBase;

        void GetMoney<T>(T fromAccount, decimal amount) where T : BankAccountBase;

        void Deposit(decimal amount);

        void OpenAccount();

        void CloseAccount();
    }
}