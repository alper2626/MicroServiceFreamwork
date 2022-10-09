namespace EntityBase.Exceptions
{
    public class CustomErrorException : Exception
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public CustomErrorException(string message,int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}
