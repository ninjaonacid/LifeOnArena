using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.Logic.TreasureChest
{
    public class LootView : MonoBehaviour
    {
        private Transform _player;
        public Action LootReady;
        public void MoveTo(Transform target)
        {
            transform.DOMove(new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z), 1f);
        }

        public void LootSpawnLogic()
        {
            Tween rotationTween = transform.DOShakeRotation(2f, 90f, 10, 90f);
            Tween moveTween = transform.DOLocalMove(new Vector3(0, 5, 0), 2f);

            Tween sequence = DOTween.Sequence()
                .Join(moveTween)
                .Join(rotationTween)
                .OnComplete(() =>
                {
                    LootReady?.Invoke();
                    Destroy(gameObject);
                });

        }

        public void SetTarget(Transform targetTransform)
        {
            _player = targetTransform;
        }
    }
}
