using UnityEngine;

namespace Code.Entity.Hero
{
    public class PointOfRotation : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
