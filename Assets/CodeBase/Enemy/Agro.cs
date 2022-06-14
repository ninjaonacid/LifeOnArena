using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Agro : MonoBehaviour
    {
        private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        public float Cooldown = 2;
        public AgentMoveToPlayer Follow;
        public TriggerObserver TriggerObserver;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;
            Follow.enabled = false;
        }

        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                StopAgroCoroutine();
                SwitchFollowOn();
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;

                _aggroCoroutine = StartCoroutine(SwitchOffColldown());
            }
        }

        private void StopAgroCoroutine()
        {
            if (_aggroCoroutine != null) StopCoroutine(_aggroCoroutine);
        }

        private IEnumerator SwitchOffColldown()
        {
            yield return new WaitForSeconds(Cooldown);
            SwitchFollowOff();
        }

        private void SwitchFollowOff()
        {
            Follow.enabled = false;
        }

        private void SwitchFollowOn()
        {
            Follow.enabled = true;
        }
    }
}