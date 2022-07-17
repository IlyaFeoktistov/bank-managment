namespace AppServicesLibrary.Services.Messenger
{
    internal class MessengerKey
    {
        public MessengerKey(object recipient, object? context)
        {
            Recipient = recipient;
            Context = context;
        }

        public object Recipient { get; private set; }
        public object? Context { get; private set; }

        protected bool Equals(MessengerKey other)
        {
            return Equals(Recipient, other.Recipient) && Equals(Context, other.Context);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((MessengerKey)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Recipient != null ? Recipient.GetHashCode() : 0) * 397) ^ (Context != null ? Context.GetHashCode() : 0);
            }
        }
    }
}
