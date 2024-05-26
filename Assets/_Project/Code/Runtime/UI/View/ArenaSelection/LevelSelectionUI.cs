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

        private Subject<LevelSelectionUI> _subject;
        public void UpdateData(Sprite icon, bool isUnlocked)
        {
            _levelImage.sprite = icon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _subject?.OnNext(this);
        }

        public IObservable<LevelSelectionUI> OnClickAsObservable()
        {
            return _subject ??= new Subject<LevelSelectionUI>();
        }
    }
}
