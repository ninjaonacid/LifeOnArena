using UnityEngine;

namespace Code.Runtime.Entity.Enemy.RangedEnemy
{
    public class CheckCastRange : MonoBehaviour
    {
        [SerializeField] private EnemyCastComponent _castComponent;
        [SerializeField] private TriggerObserver _rangeTrigger;

        private void OnEnable()
        {
            _rangeTrigger.TriggerEnter += RangeEnter;
            _rangeTrigger.TriggerExit += RangeExit;

            _castComponent.DisableCast();
        }

        private void OnDisable()
        {
            _rangeTrigger.TriggerEnter -= RangeEnter;
            _rangeTrigger.TriggerExit -= RangeExit;
        }

        private void RangeExit(Collider obj)
        {
            _castComponent.DisableCast();
        }

        private void RangeEnter(Collider obj)
        {
            _castComponent.EnableCast();
        }

    }
}