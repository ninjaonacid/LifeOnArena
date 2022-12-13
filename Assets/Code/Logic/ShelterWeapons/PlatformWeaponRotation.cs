using DG.Tweening;
using UnityEngine;

namespace Code.Logic.ShelterWeapons
{
    public class PlatformWeaponRotation : MonoBehaviour
    {
        private void Update()
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }
    }
}
