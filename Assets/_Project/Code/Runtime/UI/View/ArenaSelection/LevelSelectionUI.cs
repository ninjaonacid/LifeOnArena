using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.ArenaSelection
{
    public class LevelSelectionUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _levelImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Image _selectionFrame;
        [SerializeField] private TextMeshProUGUI _locationName;

        private Subject<LevelSelectionUI> _subject;
        public void UpdateData(Sprite icon, string locationName, bool isUnlocked)
        {
            //_levelImage.sprite = icon;
            _locationName.SetText(locationName);
            _lockImage.gameObject.SetActive(!isUnlocked);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _subject?.OnNext(this);
        }

        public IObservable<LevelSelectionUI> OnClickAsObservable()
        {
            return _subject ??= new Subject<LevelSelectionUI>();
        }
        
        public void Select()
        {
            _selectionFrame.gameObject.SetActive(true);
        }

        public void Deselect()
        {
            _selectionFrame.gameObject.SetActive(false);
        }
    }
}
