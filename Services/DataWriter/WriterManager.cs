using BankManagment.Services.DataWriter.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagment.Services.DataWriter
{
    internal class WriterManager
    {
        private readonly IDataWriter writer;

        public WriterManager(IDataWriter writer)
        {
            this.writer = writer;
        }

        public void GetData(object data)
        {
            writer.GetData(data);
        }

        public void Write()
        {
            if(writer.DataHasValue())
            {
                writer.Write();
            }
        }
    }
}
