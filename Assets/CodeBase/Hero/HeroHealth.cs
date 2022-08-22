using System;
using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using UnityEngine;


namespace CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
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
            _heroHp = progress.heroHeroHp;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.heroHeroHp.CurrentHP = Current;
            progress.heroHeroHp.MaxHP = Max;
        }
    }
}