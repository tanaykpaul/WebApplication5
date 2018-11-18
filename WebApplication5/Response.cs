namespace Readify
{
    public class Response
    {
        public string Status { get; set; }
        public ResponseCode Code { get; set; }
        public string Message { get; set; }
        public Result Result { get; set; }
    }
}