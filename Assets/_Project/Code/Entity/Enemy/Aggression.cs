using System;
using UnityEngine;

namespace Code.Entity.Enemy
{
    public class Aggression : MonoBehaviour
    {
 
        private bool _hasTarget;
        public bool HasTarget => _hasTarget;

        public float Cooldown = 2;
        public TriggerObserver TriggerObserver;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;
        }

        private void TriggerEnter(Collider obj)
        {
            if (!_hasTarget)
            {
                _hasTarget = true;
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasTarget)
            {
                _hasTarget = false;
            }
        }

        private void OnDisable()
        {
            _hasTarget = false;
        }
    }
}