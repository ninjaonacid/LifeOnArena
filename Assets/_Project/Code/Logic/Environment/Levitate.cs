using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Logic.Environment
{
    public class Levitate : MonoBehaviour
    {
        [SerializeField] private float capY = 6;


        private void Start()
        {
            transform
                .DOMoveY(Random.Range(0, capY), 5)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject);
            
            transform
                .DORotate(new Vector3(0, 0, 180), 4, RotateMode.Fast)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject);
        }
    }
}