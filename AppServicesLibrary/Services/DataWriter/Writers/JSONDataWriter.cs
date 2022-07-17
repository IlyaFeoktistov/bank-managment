using AppServicesLibrary.Services.DataWriter.Abstract;
using Newtonsoft.Json;

namespace AppServicesLibrary.Services.DataWriter.Writers
{
    public class JSONDataWriter : IDataWriter
    {
        private object? data;
        private readonly string fileName;

        public JSONDataWriter(string fileName)
        {
            this.fileName = fileName;
        }
        public bool DataHasValue()
        {
            return data != null;
        }

        public void GetData(object data)
        {
            this.data = data;
        }

        public void Write()
        {
            string json = JsonConvert.SerializeObject(data, 
                new JsonSerializerSettings 
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    //ReferenceLoopHandling = ReferenceLoopHandling.Ignore 
                });

            if(File.Exists(fileName))
                File.WriteAllText(fileName, String.Empty);

            using FileStream fileStream = new(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            using StreamWriter writer = new(fileStream);

            writer.Write(json);

        }
    }
}
