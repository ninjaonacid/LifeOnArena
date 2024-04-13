using System;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AreaOfEffect : MonoBehaviour
    {
        public event Action<GameObject> OnEnter;
        public event Action<GameObject> OnExit;

        private LayerMask _targetLayer;
        
        
        [SerializeField] private Collider _area;
        
        public AreaOfEffect SetTargetLayer(LayerMask layer)
        {
            _targetLayer = layer;
            return this;
        }

        public void SetOwnerLayer(LayerMask owner)
        {
            gameObject.layer = owner;

            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.layer = owner;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_targetLayer == 1 << other.gameObject.layer)
            {
                OnEnter?.Invoke(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_targetLayer == 1 << other.gameObject.layer)
            {
                OnExit?.Invoke(other.gameObject);
            }
        }
    }
}
