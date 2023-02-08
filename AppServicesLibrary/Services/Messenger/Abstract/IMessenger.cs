namespace AppServicesLibrary.Services.Messenger.Abstract
{
    internal interface IMessenger
    {
        public void Register<T>(object recipient, Action<T> action, object? context = null);

        public void UnRegister(object recipient, object? context = null);

        public void Send<T>(T message, object? context = null);
    }
}
