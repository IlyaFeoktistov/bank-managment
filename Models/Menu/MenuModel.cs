using BankManagment.Models.Menu.Abstract;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BankManagment.Models.Menu
{
    internal class MenuModel : IEnumerable
    {
        public MenuModel(ObservableCollection<MenuItem> menuItems)
        {
            MenuItems = menuItems;
        }

        public ObservableCollection<MenuItem> MenuItems;

        public IEnumerator GetEnumerator() => MenuItems.GetEnumerator();
    }
}
