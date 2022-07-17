using BankManagment.Infrastructure.LoginManager.Abstract;
using BankManagment.Models.Users.Abstract;

namespace BankManagment.Infrastructure.LoginManager
{
    public class LoginManager : ILoginManager
    {
        private User? currentUser;

        private static LoginManager? instance;

        public event ILoginManager.LoginManagerEventHandler? LoggedIn;
        public event ILoginManager.LoginManagerEventHandler? LoggedOut;

        public LoginManager()
        {
        }

        public static LoginManager Default {  get => instance ??= new(); }

        public User? CurrentUser { get => currentUser; }

        public void Login(User user)
        {
            currentUser = user;
            LoggedIn?.Invoke(this, new LoginManagerEventArgs(currentUser));
        }

        public void Logout()
        {
            currentUser = null;
            LoggedOut?.Invoke(this, new LoginManagerEventArgs(currentUser));
        }
    }
}
