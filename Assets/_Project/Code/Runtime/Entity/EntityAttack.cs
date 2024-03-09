using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Entity
{
    public class EntityAttack : MonoBehaviour
    {
        [SerializeField] protected LayerMask _attackLayer;
        [SerializeField] protected StatController _stats;
    }
}