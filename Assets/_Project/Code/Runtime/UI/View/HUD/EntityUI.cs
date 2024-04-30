using Code.Runtime.Entity.EntitiesComponents;
using UnityEngine;

namespace Code.Runtime.UI.View.HUD
{
    public class EntityUI : MonoBehaviour
    {
        private IDamageable _damageable;
        public StatusBar StatusBar;
        
        public void Construct(IDamageable damageable)
        {
            _damageable = damageable;

            _damageable.Health.CurrentValueChanged += UpdateHpBar;
        }

        private void OnDestroy()
        {
           _damageable.Health.CurrentValueChanged -= UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            StatusBar.SetHpValue(_damageable.Health.CurrentValue, _damageable.Health.Value);
        }
    }
}