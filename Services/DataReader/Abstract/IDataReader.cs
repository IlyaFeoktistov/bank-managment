using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagment.Services.DataReader.Abstract
{
    internal interface IDataReader
    {
        public T? Read<T>();
    }
}
