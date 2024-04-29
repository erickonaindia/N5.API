namespace N5.Application.Commons.Dtos
{
    public class BadRequestResponseDTO
    {
        public string? Message { get; set; }

        public int? Status { get; set; }

        public BadRequestResponseDTO(string message)
        {
            Status = 400;
            Message = message;
        }
    }
}
