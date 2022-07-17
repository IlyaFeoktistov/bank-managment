namespace BankManagment.Infrastructure.Repositories
{
    internal class Repository
    {
        private static ClientRepository? clientRepository;
        private static AccountRepository? accountRepository;

        public Repository() { }

        public static ClientRepository ClientRepository
        {
            get => clientRepository ??= new ClientRepository();
        }

        public static AccountRepository AccountRepository
        {
            get => accountRepository ??= new AccountRepository();
        }
    }
}
