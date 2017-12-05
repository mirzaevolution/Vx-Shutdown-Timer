namespace CoreLib.Models
{
    public class MainResult
    {
        public bool Success { get; }
        public string ErrorMessage { get; }
        public MainResult(bool success, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }
    }
}
