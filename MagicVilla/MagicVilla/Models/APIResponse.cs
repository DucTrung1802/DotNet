using System.Net;

namespace MagicVilla.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }

        public void CompileResult(HttpStatusCode httpStatusCode, object result)
        {
            StatusCode = httpStatusCode;
            IsSuccess = true;
            Result = result;
            ErrorMessages = new List<string>();
        }

        public void CompileError(HttpStatusCode httpStatusCode, Exception ex)
        {
            StatusCode = httpStatusCode;
            IsSuccess = false;
            ErrorMessages = new List<string> { ex.ToString() };
        }
    }
}
