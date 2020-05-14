using System.Net;

namespace ProjectHealthReport.Web.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(string error, HttpStatusCode httpStatusCode, object info)
        {
            Error = error;
            HttpStatusCode = httpStatusCode;
            Info = info;
        }

        public string Error { get; }
        public HttpStatusCode HttpStatusCode { get; }

        public object Info { get; set; }
    }
}