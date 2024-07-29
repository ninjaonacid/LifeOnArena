using Code.Runtime.Entity.Hero;
using UnityEngine;

namespace Code.Runtime.Logic.Environment
{
    public class FallDie : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<HeroDeath>().ForceDeath();
        }
    }
}
