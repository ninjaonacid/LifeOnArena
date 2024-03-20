namespace Code.Runtime.UI.Model.DTO
{
    public class TimerMessageDto : IScreenModelDto
    {
        public string Message;
        public float Seconds;

        public TimerMessageDto(string message, float seconds)
        {
            Message = message;
            Seconds = seconds;
        }
    }
}