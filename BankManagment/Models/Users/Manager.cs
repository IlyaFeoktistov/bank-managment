using BankManagment.Models.Users.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagment.Models.Users
{
    internal class Manager : User
    {
        public override string Name { get => "Менеджер"; }
    }
}
