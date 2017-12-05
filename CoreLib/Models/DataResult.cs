namespace CoreLib.Models
{
    public class DataResult<T>
    {
        public T Data { get; }
        public MainResult Status { get; }
        public DataResult(T data, MainResult status)
        {
            Data = data;
            Status = status;
        }
    }
}
