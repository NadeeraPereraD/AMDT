namespace AMDT.API.Exceptions
{
    public class UnauthorizedAccessAppException : Exception
    {
        public UnauthorizedAccessAppException(string message) : base(message) { }
    }
}
