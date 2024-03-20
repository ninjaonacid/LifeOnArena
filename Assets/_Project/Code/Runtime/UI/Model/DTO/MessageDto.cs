namespace Code.Runtime.UI.Model.DTO
{
    public class MessageDto : IScreenModelDto
    {
        public string Message { get; set; }
        public MessageDto(string message)
        {
            Message = message;
        }

    }
}