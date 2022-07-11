namespace BankManagment.Models.BankAccount.Abstract
{
    public interface IBankAccount
    {
        void SendMoney<T>(T toAccount, float amount) where T : BankAccountBase;

        void GetMoney<T>(T fromAccount, float amount) where T : BankAccountBase;

        void Deposit(float amount);

        void OpenAccount();

        void CloseAccount();
    }
}