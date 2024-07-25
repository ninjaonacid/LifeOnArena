using PrimeTween;
using UnityEngine;

namespace Code.Runtime.Logic.Environment
{
    public class FlyingObelisk : MonoBehaviour
    {
        [SerializeField] private Transform _topObelisk;
        [SerializeField] private Transform _bottomObelisk;
        private void Start()
        {
            _topObelisk.DOLocalMove(new Vector3(0, 4, 0), 2f).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
            _bottomObelisk.DOLocalMove(new Vector3(0, 2, 0), 2f).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
            _topObelisk.DOShakeRotation(3f, 5).SetLoops(-1, LoopType.Yoyo);
            _bottomObelisk.DOShakeRotation(3f, 5).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
