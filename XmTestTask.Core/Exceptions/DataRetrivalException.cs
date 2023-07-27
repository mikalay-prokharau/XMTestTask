namespace XmTestTask.Core.Exceptions
{
    public class DataRetrivalException : Exception
    {
        public DataRetrivalException()
        {
        }

        public DataRetrivalException(string message)
            : base(message)
        {
        }

        public DataRetrivalException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
