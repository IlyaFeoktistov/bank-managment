using AppServicesLibrary.Services.DataWriter.Abstract;

namespace AppServicesLibrary.Services.DataWriter
{
    public class WriterManager
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
