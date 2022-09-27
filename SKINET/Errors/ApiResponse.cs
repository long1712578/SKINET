namespace SKINET.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatus(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        
        private string GetDefaultMessageForStatus(int statusCode)
        {
            return statusCode switch
            {
                400 => "A badrequest, you are made",
                401 => "Authorizer, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side",
                _ => ""
            };
        }
    }
}
