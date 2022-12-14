using System;
using Code.Data;
using Code.Logic;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Hero
{
    public class HeroHealth : MonoBehaviour, ISave, IHealth
    {
        private HeroHP _heroHp;

        public event Action HealthChanged;

        public float Current
        {
            get => _heroHp.CurrentHP;
            set
            {
                if (_heroHp.CurrentHP != value)
                {
                    _heroHp.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _heroHp.MaxHP;
            set => _heroHp.MaxHP = value;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;
            Current -= damage;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _heroHp = progress.HeroHp;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroHp.CurrentHP = Current;
            progress.HeroHp.MaxHP = Max;
        }
    }
}