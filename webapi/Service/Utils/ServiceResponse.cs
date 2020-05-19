namespace Service.Utils
{
    public class ServiceResponse
    {
        // SetSuccess
        public ServiceResponse()
        {
            Error = false;
        }

        // SetSuccess
        public ServiceResponse(object data)
        {
            Error = false;
            Data = data;
        }

        // SetErrror
        public ServiceResponse(string message)
        {
            Error = true;
            ErrorMessage = message;
        }

        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }
}
