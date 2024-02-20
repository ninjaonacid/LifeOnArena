using System.Collections.Generic;
using Code.Entity.StatusEffects;
using Code.Logic.Collision;
using UnityEngine;

namespace Code.Entity.EntitiesComponents
{
    public interface IAttack
    {
        void AoeAbilityAttack(Vector3 castPoint, IReadOnlyList<StatusEffect> statusEffects);
        void InvokeHit(int hitCount);
    }
}
