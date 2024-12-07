namespace AMS.Domain._SharedKernel.DTOs.Response
{
    public class ApiResponse<T>
    {
        public T ResponseData { get; set; }
        public string CommandMessage { get; set; }
    }
}
