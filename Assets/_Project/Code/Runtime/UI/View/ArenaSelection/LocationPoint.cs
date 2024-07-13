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
        [SerializeField] private Color32 _completedColor;

        private Vector3 _markerOriginalScale;
        
        private Subject<LocationPoint> _subject;
        private Tween _tween;
        private bool _isUnlocked;
        private bool _isCompleted;

        private void Awake()
        {
            _markerOriginalScale = _markerIcon.transform.localScale;
        }

        public bool IsUnlocked() => _isUnlocked;

        public void UpdateData(bool isUnlocked, bool isCompleted)
        {
            _isUnlocked = isUnlocked;
            _isCompleted = isCompleted;
            
            if (isUnlocked)
            {
                SetColor(_unlockedColor);
            }
            else
            {
                SetLineColor(_lockedColor);
            }

            if (isCompleted)
            {
                SetColor(_completedColor);
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
            if (_isUnlocked)
            {
                SetMarkerColor(_highlightColor);
            } 
            else if (!_isUnlocked)
            {
                SetMarkerColor(_lockedColor);
            } 
            

            if (_tween != null)
            {
                _tween.Kill();
            }
            
            ResetState();
            
            _tween = _markerIcon.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }

        public void Deselect()
        {
            if (_isUnlocked && !_isCompleted)
            {
                SetColor(_baseColor);
            }
            else if (!_isUnlocked)
            {
                SetLineColor(_lockedColor);
                SetMarkerColor(_baseColor);
            }
            else if (_isUnlocked && _isCompleted)
            {
                SetColor(_completedColor);
            }

            _markerIcon.transform.localScale = _markerOriginalScale;
            
            if (_tween != null)
            {
                _tween.Kill();
            }
        }

        private void ResetState()
        {
            _markerIcon.transform.localScale = _markerOriginalScale;
        }

        private void SetColor(Color32 color)
        {
            _markerIcon.color = color;
            _rayIcon.color = color;
            _line.color = color;
        }

        private void SetLineColor(Color32 color)
        {
            _line.color = color;
        }

        private void SetMarkerColor(Color32 color)
        {
            _rayIcon.color = color;
            _markerIcon.color = color;
        }
    }
}
