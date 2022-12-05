using DG.Tweening;
using UnityEngine;

namespace Code.Logic.ShelterWeapons
{
    public class PlatformWeaponRotation : MonoBehaviour
    {
        private void Start()
        {
            transform.DORotate(new Vector3(0, 180, 0), 5f).SetLoops(-1, LoopType.Incremental);
        }

    }
}
