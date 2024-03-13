using System;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Entity
{
    public class EntityAttack : MonoBehaviour, IAttackComponent
    {
        public event Action<int> OnHit;
        
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected StatController _stats;
        [SerializeField] protected EntityWeapon _entityWeapon;

        public LayerMask GetTargetLayer()
        {
            return _targetLayer;
        }

        public LayerMask GetLayer()
        {
            return _entityWeapon.GetEquippedWeapon().gameObject.layer;
        }
        
        public void InvokeHit(int hitCount)
        {
            OnHit?.Invoke(hitCount);
        }
    }
}