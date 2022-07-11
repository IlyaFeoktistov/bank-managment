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

            //MainWindow mainWindow = new()
            //{
            //    DataContext = new MainWindowVM()
            //    {
            //        RequestClose = new System.Action(() => MainWindow.Close())
            //    }
            //};

            LoginWindowVM loginWindowVM = new();
            LoginWindow loginWindow = new();

            loginWindowVM.RequestClose += delegate { loginWindow.Close(); };
            loginWindow.DataContext = loginWindowVM;

            loginWindow.Show();
            //loginWindow.Close();
            //mainWindow.Show();
        }
    }
}
