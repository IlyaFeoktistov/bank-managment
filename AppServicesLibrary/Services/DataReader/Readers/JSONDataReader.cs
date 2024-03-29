﻿using AppServicesLibrary.Services.DataReader.Abstract;
using Newtonsoft.Json;

namespace AppServicesLibrary.Services.DataReader
{
    public class JSONDataReader : IDataReader
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
