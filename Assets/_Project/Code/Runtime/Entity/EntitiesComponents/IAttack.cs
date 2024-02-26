using System.Collections.Generic;
using Code.Runtime.Entity.StatusEffects;
using UnityEngine;

namespace Code.Runtime.Entity.EntitiesComponents
{
    public interface IAttack
    {
        void AoeAbilityAttack(Vector3 castPoint, IReadOnlyList<GameplayEffect> statusEffects);
        void InvokeHit(int hitCount);
    }
}
