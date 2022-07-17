using BankManagment.Models.Users.Abstract;

namespace BankManagment.Infrastructure.LoginManager
{
    public class LoginManagerEventArgs
    {
        public LoginManagerEventArgs(User? user)
        {
            User = user;
        }

        public User? User { get; }
    }
}
