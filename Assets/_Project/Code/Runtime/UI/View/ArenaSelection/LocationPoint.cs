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
        [SerializeField] private Image _line;
        [SerializeField] private Image _markerIcon;
        [SerializeField] private LevelIdentifier LevelId;

        private Subject<LocationPoint> _subject;
        
        public void UpdateData(Sprite icon, string locationName, bool isUnlocked)
        {
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
            //_selectionFrame.gameObject.SetActive(true);
        }

        public void Deselect()
        {
            //_selectionFrame.gameObject.SetActive(false);
        }
    }
}
