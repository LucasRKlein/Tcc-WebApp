namespace TccApp.Domain.Dtos
{
    public class BackResponseListDto<T>
    {
        public bool Success { get; set; }
        public List<T> Data { get; set; }
        public string Errors { get; set; }
    }
}
