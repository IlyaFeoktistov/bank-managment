using BankManagment.Models.Users.Abstract;

namespace BankManagment.Infrastructure.LoginManager.Abstract
{
    public interface ILoginManager
    {
        delegate void LoginManagerEventHandler(object? sender, LoginManagerEventArgs e);

        event LoginManagerEventHandler LoggedIn;
        event LoginManagerEventHandler LoggedOut;

        void Login(User user);
        void Logout();
    }
}
