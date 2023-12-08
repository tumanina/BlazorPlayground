namespace BlazorPlayground.Web.Client
{
    public class ClientException : Exception
    {
        public ClientException(int statusCode, string message)
            : base($"Server returned unexpected status code {statusCode} with message: {message}")
        {
        }
    }
}
