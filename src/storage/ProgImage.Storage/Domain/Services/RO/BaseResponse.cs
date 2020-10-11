namespace ProgImage.Storage.Domain.Services.RO
{
    public abstract class BaseResponse
    {
        protected BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}