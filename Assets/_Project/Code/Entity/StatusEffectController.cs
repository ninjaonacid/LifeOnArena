using System.Collections.Generic;
using Code.Entity.StatusEffects;
using UnityEngine;

namespace Code.Entity
{
    public class StatusEffectController : MonoBehaviour
    {
        private List<IStatusEffect> _statusEffects;

        public void AddEffect(IStatusEffect effect)
        {
            if (_statusEffects.Contains(effect))
            {
                return;
            }
            _statusEffects.Add(effect);
        }

        public void RemoveEffect(IStatusEffect effect)
        {
            if (_statusEffects.Contains(effect))
            {
                _statusEffects.Remove(effect);
            }
        }

        public bool IsDisabled()
        {
            foreach (var status in _statusEffects)
            {
                if (status is StunStatusEffect stun)
                {
                    
                }
            }

            return false;
        }
    }
}