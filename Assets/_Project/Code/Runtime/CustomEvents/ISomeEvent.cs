namespace Code.Runtime.CustomEvents
{
    public interface ISomeEvent : IEvent
    {
        public string Message { get; set; }
    }
}
