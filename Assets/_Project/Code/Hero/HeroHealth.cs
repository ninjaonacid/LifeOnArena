using System;
using Code.Data;
using Code.Logic;
using Code.Logic.EntitiesComponents;
using Code.Services.PersistentProgress;
using Code.StaticData.StatSystem;
using UnityEngine;

namespace Code.Hero
{
    public class HeroHealth : MonoBehaviour, ISave, IHealth
    {
        private CharacterStats _characterStats;
        public Stat Health { get; set; }
        public event Action HealthChanged;

        public float Current
        {
            get => _characterStats.CurrentHP;
            set
            {
                if (_characterStats.CurrentHP != value)
                {
                    _characterStats.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _characterStats.CalculateHeroHealth();
            set => _characterStats.BaseMaxHP = value;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;
            Current -= damage;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _characterStats = progress.CharacterStats;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.CharacterStats.CurrentHP = Current;
            progress.CharacterStats.BaseMaxHP = Max;
        }
    }
}