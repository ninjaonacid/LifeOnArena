using System;
using Code.Runtime.ConfigData.Identifiers;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.ArenaSelection
{
    public class LocationPoint : MonoBehaviour, IPointerClickHandler
    {
        public LevelIdentifier LevelId;
        [SerializeField] private Image _line;
        [SerializeField] private Image _markerIcon;
        [SerializeField] private Image _rayIcon;
        [SerializeField] private Color32 _highlightColor;
        [SerializeField] private Color32 _baseColor;
        [SerializeField] private Color32 _lockedColor;
        [SerializeField] private Color32 _unlockedColor;

        private Vector3 _markerOriginalScale;
        
        private Subject<LocationPoint> _subject;
        private Tween _tween;

        private void Start()
        {
            _markerOriginalScale = _markerIcon.transform.localScale;
        }

        public void UpdateData(bool isUnlocked)
        {
            if (isUnlocked)
            {
                SetColor(_unlockedColor);
            }
            else
            {
                SetColor(_lockedColor);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _subject?.OnNext(this);
        }

        public IObservable<LocationPoint> OnClickAsObservable()
        {
            return _subject ??= new Subject<LocationPoint>();
        }
        
        public void Select()
        {
            _markerIcon.color = _highlightColor;
            _rayIcon.color = _highlightColor;

            if (_tween != null)
            {
                _tween.Kill();
            }
            
            _tween = _markerIcon.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }

        public void Deselect()
        {
            SetColor(_baseColor);

            _markerIcon.transform.localScale = _markerOriginalScale;
            
            if (_tween != null)
            {
                _tween.Kill();
            }
        }

        private void SetColor(Color32 color)
        {
            _markerIcon.color = color;
            _rayIcon.color = color;
        }
    }
}
