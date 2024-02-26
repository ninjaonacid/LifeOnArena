using System;
using Newtonsoft.Json;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class PositionOnLevel
    {
        public string Level;
        public Vector3Data Position;

        [JsonConstructor]
        public PositionOnLevel(string level, Vector3Data position)
        {
            Level = level;
            Position = position;
        }

        public PositionOnLevel(string initialLevel)
        {
            Level = initialLevel;
        }
    }
}