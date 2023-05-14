using Code.Logic;
using Code.Logic.EntitiesComponents;
using UnityEngine;

namespace Code.UI.HUD
{
    public class ActorUI : MonoBehaviour
    {
        private IHealth _health;
        public HpBar HpBar;


        public void Construct(IHealth health)
        {
            _health = health;

            _health.Health.CurrentValueChanged += UpdateHpBar;
        }

        private void OnDestroy()
        {
           _health.Health.CurrentValueChanged -= UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            HpBar.SetValue(_health.Health.CurrentValue, _health.Health.Value);
        }
    }
}