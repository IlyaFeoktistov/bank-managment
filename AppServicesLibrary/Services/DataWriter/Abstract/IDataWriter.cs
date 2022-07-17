namespace AppServicesLibrary.Services.DataWriter.Abstract
{
    public interface IDataWriter
    {
        void GetData(object data);
        void Write();
        bool DataHasValue();
    }
}
