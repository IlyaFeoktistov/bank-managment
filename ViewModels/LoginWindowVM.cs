using BankManagment.Services.LoginManager;
using BankManagment.Models.Users;
using BankManagment.Models.Users.Abstract;
using BankManagment.Services.Command.Abstract;
using BankManagment.Services.Messenger;
using BankManagment.ViewModels.Abstract;
using BankManagment.Views;
using System.Linq;
using System.Windows.Input;

namespace BankManagment.ViewModels
{
    internal class LoginWindowVM : ViewModel
    {
        private readonly User[] users;
        private User selectedUser;
        public override string ViewModelName => "LoginWindow";

        public LoginWindowVM()
        {
            users = new User[]
            {
                new Manager(),
                new Consultant()
            };

            selectedUser = users.FirstOrDefault()!;

            LoginCommand = new RelayCommand(OnLoginCommandExecute, null);
        }

        private void OnLoginCommandExecute(object? obj)
        {
            MainWindowVM mainWindowVM = new();
            MainWindow mainWindow = new();

            mainWindowVM.RequestClose += delegate { mainWindow.Close(); };

            mainWindow.DataContext = mainWindowVM;
            
            mainWindow.Show();

            LoginManager.Default.Login(selectedUser);

            RequestClose?.Invoke();
            
        }

        public ICommand LoginCommand { get; set; }

        public User[] Users { get => users; }

        public User SelectedUser 
        {
            get => selectedUser;
            set => SetProperty(ref selectedUser, value);
        }
    }
}
