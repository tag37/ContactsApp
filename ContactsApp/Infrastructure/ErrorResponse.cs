namespace ContactsApp.Infrastructure
{
    public class ErrorResponse
    {
        public ErrorResponse(string status, string details)
        {
            Status = status;
            Details = details;
        }
        public string Status { get; }
        public string Details { get; }
    }
}
