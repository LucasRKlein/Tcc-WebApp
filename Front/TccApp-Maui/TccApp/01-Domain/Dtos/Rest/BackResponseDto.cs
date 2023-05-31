namespace TccApp.Domain.Dtos
{
    public class BackResponseDto<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Errors { get; set; }
    }
}
