using BankManagment.Models.Menu;
using BankManagment.Models.Menu.Abstract;
using BankManagment.Models.Menu.Items;
using BankManagment.Services.Command.Abstract;
using BankManagment.ViewModels.Abstract;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BankManagment.ViewModels
{
    internal class MainWindowVM : ViewModel
    {
        private MenuItem? selectedMenuItem;

        public MainWindowVM()
        {
            Menu = InitializeMenu();

            CloseApplicationCommand ??= new RelayCommand(e => {
                RequestClose?.Invoke();
            });

            selectedMenuItem = Menu.MenuItems.FirstOrDefault();

            if (selectedMenuItem != null)
                selectedMenuItem.ContentVM.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };
        }

        public override string ViewModelName => "MainWindow";

        public ICommand CloseApplicationCommand { get; private set; }

        #region Menu

        public MenuModel Menu { get; }

        public MenuItem? SelectedMenuItem 
        {
            get => selectedMenuItem;
            set => SetProperty(ref selectedMenuItem, value);
        }

        private static MenuModel InitializeMenu()
        {
            return new MenuModel(new ObservableCollection<MenuItem>()
            {
                new ClientsMenuItem(),
                new DepartamentsMenuItem()
            });
        }

        #endregion
    }
}
