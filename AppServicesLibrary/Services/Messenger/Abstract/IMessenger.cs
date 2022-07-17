namespace AppServicesLibrary.Services.Messenger.Abstract
{
    internal interface IMessenger
    {
        void Register<T>(object recipient, Action<T> action);

        void UnRegister(object recipient);

        void Send<T>(T message);
    }
}
