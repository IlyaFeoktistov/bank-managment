using AppServicesLibrary.Services.Messenger.Abstract;

namespace AppServicesLibrary.Services.Messenger
{
    public class Messenger : IMessenger
    {
        private static Messenger? instance;
        private static readonly Dictionary<MessengerKey, object> RegisteredRecipients = new ();

        private Messenger() { }

        public static Messenger Default => instance ??= new Messenger();

        public void Register<T>(object recipient, Action<T> action, object? context = null)
        {
            var key = new MessengerKey(recipient, context);
            RegisteredRecipients.Add(key, action);
        }

        public void Send<T>(T message, object? context = null)
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

        public void UnRegister(object recipient, object? context = null)
        {
            var key = new MessengerKey(recipient, context);
            RegisteredRecipients.Remove(key);
        }
    }
}
