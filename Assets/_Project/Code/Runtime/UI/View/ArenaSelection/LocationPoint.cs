using System;
using Code.Runtime.ConfigData.Identifiers;
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
        [SerializeField] private Color32 _lockedLineColor;
        [SerializeField] private Color32 _unlockedLineColor;

        private Subject<LocationPoint> _subject;
        
        public void UpdateData(bool isUnlocked)
        {
            if (isUnlocked)
            {
                
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

        }

        public void Deselect()
        {
            _markerIcon.color = _baseColor;
            _rayIcon.color = _baseColor;
        }
    }
}
