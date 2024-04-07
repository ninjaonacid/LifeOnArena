using System;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Entity
{
    public class EntityAttackComponent : MonoBehaviour, IAttackComponent
    {
        public event Action<int> OnHit;
        
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected StatController _stats;
        [SerializeField] protected EntityWeapon _entityWeapon;

        protected float _attackSpeedDivider = 10f;

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

        public float GetAttacksPerSecond()
        {
            float attackSpeed = _stats.Stats["AttackSpeed"].Value;
            
            if (attackSpeed >= 0) {
             
                float bias = 1.25f - attackSpeed / 8.0f;
                
                bias = Mathf.Clamp(bias, 0, 1);

                return attackSpeed + bias;
            }

            return 1.0f / (1.0f - attackSpeed);
        }
    }
}