namespace AppServicesLibrary.Services.DataReader.Abstract
{
    public interface IDataReader
    {
        public T? Read<T>();
    }
}
