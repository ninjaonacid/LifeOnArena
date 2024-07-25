using System;
using PrimeTween;
using UnityEngine;

namespace Code.Runtime.Logic.TreasureChest
{
    public class LootView : MonoBehaviour
    {
        public Action LootReady;

        public void LootSpawnLogic()
        {
            Tween rotationTween = transform.DOShakeRotation(2f, 90f, 10, 90f).SetLink(gameObject);
            Tween moveTween = transform.DOLocalMove(new Vector3(0, 5, 0), 2f).SetLink(gameObject);

            Sequence sequence = DOTween.Sequence()
                .Join(moveTween)
                //.Join(rotationTween)
                .OnComplete(() =>
                {
                    LootReady?.Invoke();
                    Destroy(gameObject);
                });
        }
    }
}
