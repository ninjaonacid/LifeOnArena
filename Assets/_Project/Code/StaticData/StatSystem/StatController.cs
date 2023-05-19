using System;
using System.Collections.Generic;
using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.StaticData.StatSystem
{
    public class StatController : MonoBehaviour
    {
        [SerializeField] private StatDatabase _StatDatabase;

        protected Dictionary<string, Stat> _stats = new Dictionary<string, Stat>(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, Stat> Stats => _stats;

        private bool _isInitialized;
        public bool IsInitialized => _isInitialized;
        public event Action Initialized;
        public event Action Uninitiliazed;

        protected virtual void Awake()
        {
            if (!_isInitialized)
            {
                Initialize();
            }
        }

        private void OnDestroy()
        {
            Uninitiliazed?.Invoke();
        }

        public void Initialize()
        {
            foreach (StatDefinition stat in _StatDatabase.StatDefinitions)
            {
                _stats.Add(stat.name, new Stat(stat, this));
            }

            foreach (StatDefinition stat in _StatDatabase.AttributeDefinitions)
            {
                if(stat.name.Equals("Health", StringComparison.OrdinalIgnoreCase))
                {
                    _stats.Add(stat.name, new Health(stat, this));
                }
                else
                {
                    _stats.Add(stat.name, new Attribute(stat, this)); 
                }
                
            }
            
            foreach (StatDefinition stat in _StatDatabase.PrimaryStats)
            {
                _stats.Add(stat.name, new PrimaryStat(stat, this));
            }

            foreach (var stat in Stats.Values)
            {
                stat.Initialize();
            }
            
            _isInitialized = true;
            StatsLoaded();
        }

        protected void StatsLoaded()
        {
            Initialized?.Invoke();
        }
    }
}
