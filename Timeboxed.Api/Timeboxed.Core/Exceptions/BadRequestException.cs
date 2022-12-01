namespace Timeboxed.Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message) { }

        public BadRequestException(object message)
            : base(message.ToString()) { }
    }
}
