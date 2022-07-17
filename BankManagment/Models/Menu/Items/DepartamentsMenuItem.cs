using BankManagment.Models.Menu.Abstract;
using BankManagment.ViewModels;
using BankManagment.ViewModels.Abstract;

namespace BankManagment.Models.Menu.Items
{
    internal class DepartamentsMenuItem : MenuItem
    {
        public override string Header => "Департаменты";

        public override ViewModel ContentVM => new DepartamentVM();
    }
}
