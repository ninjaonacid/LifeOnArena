using System;
using Code.Runtime.UI;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.Entity.Hero
{
    public class FloatingLevel : MonoBehaviour
    {
        [SerializeField] private Vector3 _targetPosition;
        [SerializeField] private CanvasElement _levelUpIcon;
        
        private Vector3 _originPosition;

        private void Awake()
        {
            _originPosition = transform.position;
        }

        public void LevelUpLogic()
        {
            _levelUpIcon.Show();

            transform.DOLocalMoveY( _targetPosition.y, 2f).SetLink(gameObject).OnComplete(() =>
            {
                _levelUpIcon.Hide();
                transform.position = _originPosition;
            });
        }
    }
}
