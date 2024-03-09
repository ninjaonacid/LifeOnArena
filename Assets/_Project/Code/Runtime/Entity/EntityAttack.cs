using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Entity
{
    public class EntityAttack : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private StatController _stats;
    }
}