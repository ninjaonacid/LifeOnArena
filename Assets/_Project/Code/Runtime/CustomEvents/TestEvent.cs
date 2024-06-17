namespace Code.Runtime.CustomEvents
{
    public class TestEvent : ISomeEvent
    {
        public TestEvent(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
