namespace Code.CustomEvents
{
    public interface ISomeEvent : IEvent
    {
        public string Message { get; set; }
    }
}
