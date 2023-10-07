using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Logic.Environment
{
    public class Levitate : MonoBehaviour
    {
        [SerializeField] private float capY = 20;


        private void Start()
        {
            transform.DOMoveY(capY, 5).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
