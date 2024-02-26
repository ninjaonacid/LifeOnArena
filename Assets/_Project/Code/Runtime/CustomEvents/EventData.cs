namespace Code.Runtime.CustomEvents
{

    public struct EventData : IEvent
    {
        public EventType Type;
        public EventData(EventType type)
        {
            Type = type;
        }

        public enum EventType
        {
            MonsterSpawnersCleared,
            HeroDeath
        }


    }
}
