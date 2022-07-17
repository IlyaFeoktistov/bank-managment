using AppServicesLibrary.Services.DataReader.Abstract;

namespace AppServicesLibrary.Services.DataReader
{
    public class ReaderManager
    {
        private readonly IDataReader reader;

        public ReaderManager(IDataReader reader)
        {
            this.reader = reader;
        }

        public T? Read<T>() => reader.Read<T>();
    }
}
