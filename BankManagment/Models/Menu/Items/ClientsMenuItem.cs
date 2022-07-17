using BankManagment.Models.Menu.Abstract;
using BankManagment.ViewModels;
using BankManagment.ViewModels.Abstract;

namespace BankManagment.Models.Menu.Items
{
    internal class ClientsMenuItem : MenuItem
    {
        public override string Header => "Клиенты";

        public override ViewModel ContentVM => new ClientVM();
    }
}
