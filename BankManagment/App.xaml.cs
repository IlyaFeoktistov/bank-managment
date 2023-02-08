using BankManagment.ViewModels;
using BankManagment.Views;
using System.Windows;

namespace BankManagment
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoginWindowVM loginWindowVM = new();
            LoginWindow loginWindow = new();

            loginWindowVM.RequestClose += delegate { loginWindow.Close(); };
            loginWindow.DataContext = loginWindowVM;

            loginWindow.Show();
        }
    }
}
