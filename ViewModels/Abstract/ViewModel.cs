using BankManagment.Services.NotifyPropertyChanged;
using System;

namespace BankManagment.ViewModels.Abstract
{
    internal abstract class ViewModel : NotifyPropertyChanged
    {
        public abstract string ViewModelName { get; }

        public Action? RequestClose { get; set; }
    }
}
