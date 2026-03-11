namespace Tools.Domain.Exceptions
{
    public class DomainException : Exception
    {
        
        public string Code { get; set; }
        public DomainException(string code, string message) : base(message) 
        { 
            Code = code;
        }
    }
}
