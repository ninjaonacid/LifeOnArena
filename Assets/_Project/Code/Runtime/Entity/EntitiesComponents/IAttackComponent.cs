using System;
using System.Collections.Generic;
using Code.Runtime.Entity.StatusEffects;
using UnityEngine;

namespace Code.Runtime.Entity.EntitiesComponents
{
    public interface IAttackComponent
    {
        public event Action<int> OnHit;
        public void InvokeHit(int hitCount);
    }
}
