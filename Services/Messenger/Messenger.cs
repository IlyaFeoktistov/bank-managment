using BankManagment.Services.Messenger.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankManagment.Services.Messenger
{
    public class Messenger : IMessenger
    {
        private static Messenger? instance;
        private static readonly Dictionary<MessengerKey, object> RegisteredRecipients = new ();

        private Messenger() { }

        public static Messenger Default 
        { 
            get 
            { 
                if(instance == null)
                    instance = new Messenger();

                return instance; 
            } 
        }

        public void Register<T>(object recipient, Action<T> action)
        {
            Register(recipient, action, null);
        }

        public void Register<T>(object recipient, Action<T> action, object? context)
        {
            var key = new MessengerKey(recipient, context);
            RegisteredRecipients.Add(key, action);
        }

        public void Send<T>(T message)
        {
            Send(message, null);
        }
        public void Send<T>(T message, object? context)
        {
            IEnumerable<KeyValuePair<MessengerKey, object>> result;

            if (context == null)
            {
                result = from keyValuePair in RegisteredRecipients
                         where keyValuePair.Key.Context == null
                         select keyValuePair;
            }
            else
            {
                result = from keyValuePair
                         in RegisteredRecipients
                         where keyValuePair.Key.Context != null && keyValuePair.Key.Context.Equals(context)
                         select keyValuePair;
            }

            foreach (var action in result.Select(x => x.Value).OfType<Action<T>>())
            {
                action(message);
            }
        }

        public void UnRegister(object recipient)
        {
            UnRegister(recipient, null);
        }

        public void UnRegister(object recipient, object? context)
        {
            var key = new MessengerKey(recipient, context);
            RegisteredRecipients.Remove(key);
        }
    }
}
