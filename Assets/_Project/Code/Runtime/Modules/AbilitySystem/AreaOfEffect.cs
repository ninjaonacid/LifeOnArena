using System;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AreaOfEffect : MonoBehaviour
    {
        public event Action OnEnter;
        public event Action OnExit;
        
        [SerializeField] private Collider _area;

        private void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke();
        }
    }
}
