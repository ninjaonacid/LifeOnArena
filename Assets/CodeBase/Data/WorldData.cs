using System;
using UnityEngine;

namespace CodeBase
{

    [Serializable]
    public class WorldData
    {
        
        public PositionOnLevel PositionOnLevel;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
        }
    }
}