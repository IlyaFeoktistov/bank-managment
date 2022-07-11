using System;

namespace BankManagment.Models.Clients
{
    public class Record
    {
        public Record(int id, DateTime time, string user, RecordTypes recordType, string propertyName)
        {
            Id = id;
            Time = time;
            User = user;
            RecordType = recordType;
            PropertyName = propertyName;
        }

        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string User { get; set; }
        public RecordTypes RecordType { get; set; }
        public string PropertyName { get; set; }

        public static RecordTypes GetRecordType(string oldValue, string newValue)
        {
            if(oldValue.Length == 0) 
                return RecordTypes.Added;

            if (newValue.Length == 0)
                return RecordTypes.Deleted;

            //if (oldValue.Length > 0 && newValue.Length > 0) 
                return RecordTypes.Modified;
            
            
        }
    }
}