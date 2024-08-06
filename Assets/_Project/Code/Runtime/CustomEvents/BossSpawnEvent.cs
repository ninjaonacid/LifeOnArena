using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;

namespace Code.Runtime.CustomEvents
{
    public class BossSpawnEvent : IEvent
    {
        public readonly GameObject BossGo;

        public BossSpawnEvent(GameObject bossGo)
        {
            BossGo = bossGo;
        }
    }
}
