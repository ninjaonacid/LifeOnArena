using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
        }
    }

}
