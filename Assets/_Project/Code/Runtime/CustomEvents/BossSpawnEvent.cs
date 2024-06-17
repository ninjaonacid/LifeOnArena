using System.Transactions;
using Code.Runtime.ConfigData.Identifiers;
using Sirenix.OdinInspector.Editor.Drawers;
using UnityEngine;

namespace Code.Runtime.CustomEvents
{
    public class BossSpawnEvent : IEvent
    {
        public GameObject BossGo;
        public MobIdentifier BossId;

        public BossSpawnEvent(GameObject bossGo, MobIdentifier bossId)
        {
            BossGo = bossGo;
            BossId = bossId;
        }
    }
}
