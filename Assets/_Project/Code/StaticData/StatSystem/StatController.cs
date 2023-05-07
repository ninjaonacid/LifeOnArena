using System;
using System.Collections.Generic;
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
    }
}
