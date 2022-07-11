using BankManagment.ViewModels.Abstract;

namespace BankManagment.Models.Menu.Abstract
{
    internal abstract class MenuItem
    {
        public abstract string Header { get; }
        public abstract ViewModel ContentVM { get; }
    }
}
