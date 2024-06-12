using Code.Runtime.Entity.EntitiesComponents;
using UnityEngine;

namespace Code.Runtime.UI.View.HUD
{
    public class EntityUI : MonoBehaviour
    {
        private IDamageable _damageable;
        [SerializeField] private StatusBar _statusBar;
        
        public void Construct(IDamageable damageable)
        {
            _damageable = damageable;

            _damageable.Health.CurrentValueChanged += UpdateHpBar;
        }

        public void SetActiveHpView(bool value)
        {
            _statusBar.gameObject.SetActive(value);
        }

        private void OnDestroy()
        {
           _damageable.Health.CurrentValueChanged -= UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            _statusBar.SetHpValue(_damageable.Health.CurrentValue, _damageable.Health.Value);
        }
    }
}