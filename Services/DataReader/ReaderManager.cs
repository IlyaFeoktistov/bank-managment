using BankManagment.Services.DataReader.Abstract;

namespace BankManagment.Services.DataReader
{
    internal class ReaderManager
    {
        private readonly IDataReader reader;

        public ReaderManager(IDataReader reader)
        {
            this.reader = reader;
        }

        public T? Read<T>() => reader.Read<T>();
    }
}
