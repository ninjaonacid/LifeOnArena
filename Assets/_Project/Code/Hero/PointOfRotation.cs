using System;
using UnityEngine;

namespace Code.Hero
{
    public class PointOfRotation : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
