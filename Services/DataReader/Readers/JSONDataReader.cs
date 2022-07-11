using BankManagment.Services.DataReader.Abstract;
using Newtonsoft.Json;
using System.IO;

namespace BankManagment.Services.DataReader
{
    internal class JSONDataReader : IDataReader
    {
        private readonly string fileName;

        public JSONDataReader(string fileName)
        {
            this.fileName = fileName;
        }

        public T? Read<T>()
        {
            string json;

            using (FileStream reader = new(fileName, FileMode.OpenOrCreate))
            {
                using StreamReader streamReader = new(reader);
                json = streamReader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
                //TypeNameHandling = TypeNameHandling.All,
                //NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
