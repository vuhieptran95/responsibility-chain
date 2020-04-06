using System.Net;

namespace ProjectHealthReport.Web.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(string error, HttpStatusCode httpStatusCode)
        {
            Error = error;
            HttpStatusCode = httpStatusCode;
        }

        public string Error { get; }
        public HttpStatusCode HttpStatusCode { get; }
    }
}