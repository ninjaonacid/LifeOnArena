using Code.Entity.EntitiesComponents;
using Code.Logic.EntitiesComponents;
using UnityEngine;

namespace Code.UI.View.HUD
{
    public class EntityUI : MonoBehaviour
    {
        private IDamageable _damageable;
        public HpBar HpBar;
        
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
            HpBar.SetValue(_damageable.Health.CurrentValue, _damageable.Health.Value);
        }
    }
}