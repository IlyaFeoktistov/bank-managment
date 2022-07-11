using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagment.Services.DataWriter.Abstract
{
    internal interface IDataWriter
    {
        void GetData(object data);
        void Write();
        bool DataHasValue();
    }
}
