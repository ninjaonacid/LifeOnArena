namespace Code.Runtime.UI.Model
{
    public class MessageWindowDto : IScreenModelDto
    {
        public MessageWindowDto(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}