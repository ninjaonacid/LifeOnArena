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

        private void Initialize()
        {
            foreach (StatDefinition stat in _StatDatabase.StatDefinitions)
            {
                _stats.Add(stat.name, new Stat(stat));
            }

            foreach (StatDefinition stat in _StatDatabase.AttributeDefinitions)
            {
                _stats.Add(stat.name, new Attribute(stat));
            }
            
            foreach (StatDefinition stat in _StatDatabase.PrimaryStats)
            {
                _stats.Add(stat.name, new PrimaryStat(stat));
            }
        }
        
    }
}
