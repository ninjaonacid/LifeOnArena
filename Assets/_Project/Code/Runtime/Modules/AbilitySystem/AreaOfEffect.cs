using System;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AreaOfEffect : MonoBehaviour
    {
        public event Action<GameObject> OnEnter;
        public event Action<GameObject> OnExit;
        
        [SerializeField] private Collider _area;

        private void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke(other.gameObject);
        }
    }
}
