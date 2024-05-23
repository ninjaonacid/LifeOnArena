using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.AbilityMenu
{
    public class AbilityTreeCell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _abilitySlotFrame;
        [SerializeField] private Image _selectionFrame;
        [SerializeField] private Image _lockIcon;
        [SerializeField] private AbilityTreeLinePathUI _line;
        [SerializeField] private AbilityItemView _abilityItem;

        private Subject<AbilityTreeCell> _subject;
        
        public void Select()
        {
            _selectionFrame.enabled = true;
        }

        public void Deselect()
        {
            _selectionFrame.enabled = false;
        }

        public void UpdateData(Sprite abilityIcon, bool isUnlocked, int equippedSlot)
        {
            _abilityItem.SetData(abilityIcon, isUnlocked, equippedSlot);

            _line.ChangeColor(isUnlocked);
        }

        public IObservable<AbilityTreeCell> OnClickAsObservable()
        {
            return _subject ??= (_subject = new Subject<AbilityTreeCell>());
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _subject?.OnNext(this);
            Debug.Log("clicked");
        }
    }
}
