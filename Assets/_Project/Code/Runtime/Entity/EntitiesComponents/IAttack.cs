using System.Collections.Generic;
using Code.Runtime.Entity.StatusEffects;
using UnityEngine;

namespace Code.Runtime.Entity.EntitiesComponents
{
    public interface IAttack
    {
        void InvokeHit(int hitCount);
    }
}
