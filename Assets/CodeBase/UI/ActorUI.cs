using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        private IHealth _health;
        public HpBar HpBar;


        public void Construct(IHealth health)
        {
            _health = health;

            _health.HealthChanged += UpdateHpBar;
        }

        private void OnDestroy()
        {
           _health.HealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            HpBar.SetValue(_health.Current, _health.Max);
        }
    }
}