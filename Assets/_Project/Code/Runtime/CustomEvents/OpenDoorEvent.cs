namespace Code.Runtime.CustomEvents
{
    public class OpenDoorEvent : IEvent
    {
        public string Message;
        public OpenDoorEvent(string message)
        {
            Message = message;
        }
    }
}
