namespace Tools.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public IEnumerable<string> Message { get; set; }
        public DomainException(string message) : base(message)
        {
            Message = new[] { message };
        }

        public DomainException(IEnumerable<string> messages)
        {
            Message = messages;
        }
    }
}
