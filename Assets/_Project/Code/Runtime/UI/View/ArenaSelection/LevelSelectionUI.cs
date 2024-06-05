using System;
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

        private Subject<LevelSelectionUI> _subject;
        public void UpdateData(Sprite icon, bool isUnlocked)
        {
            _levelImage.sprite = icon;
            
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
