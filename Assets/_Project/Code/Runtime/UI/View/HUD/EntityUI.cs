using Code.Runtime.Entity.EntitiesComponents;
using UniRx;
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

            _damageable.Health.ObserveEveryValueChanged(x => x.CurrentValue)
                .Subscribe(x => UpdateHpBar())
                .AddTo(this);
        }

        public void SetActiveHpView(bool value)
        {
            _statusBar.gameObject.SetActive(value);
        }
        
        private void UpdateHpBar()
        {
            _statusBar.SetHpValue(_damageable.Health.CurrentValue, _damageable.Health.Value);
        }
    }
}