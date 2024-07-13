using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Modules.StatSystem
{
    public class StatController : MonoBehaviour
    {
        [SerializeField] protected StatDatabase _statDatabase;
        
        protected Dictionary<string, Stat> _stats = new Dictionary<string, Stat>(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, Stat> Stats => _stats;

        private bool _isInitialized;
        public bool IsInitialized => _isInitialized;
        public event Action Initialized;
        public event Action Uninitialized;
        

        protected virtual void Awake()
        {
            if (!_isInitialized)
            {
                Initialize();
            }
        }

        private void OnDestroy()
        {
            Uninitialized?.Invoke();
        }

        protected void Initialize()
        {
            
            foreach (StatDefinition stat in _statDatabase.StatDefinitions)
            {
                _stats.Add(stat.name, new Stat(stat, this));
            }

            foreach (StatDefinition stat in _statDatabase.AttributeDefinitions)
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
            
            foreach (StatDefinition stat in _statDatabase.PrimaryStats)
            {
                _stats.Add(stat.name, new PrimaryStat(stat, this));
            }

            foreach (var stat in Stats.Values)
            {
                stat.Initialize();
            }
            
            _isInitialized = true;
            StatsInitialized();
        }

        protected void StatsInitialized()
        {
            Initialized?.Invoke();
        }
    }
}
