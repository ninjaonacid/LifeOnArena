using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Entity
{
    public class EntityAttack : MonoBehaviour
    {
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected StatController _stats;
        [SerializeField] protected EntityWeapon _entityWeapon;

        public LayerMask GetTargetLayer()
        {
            return _targetLayer;
        }
    }
}