using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.AbilityMenu
{
    public class AbilityTreeCell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private CanvasElement _selectionFrame;
        [SerializeField] private CanvasElement _equippedFrame;
        [SerializeField] private CanvasElement _lockIcon;
        [SerializeField] private AbilityTreeLinePathUI _line;
        [SerializeField] private AbilityItemView _abilityItem;

        private Subject<AbilityTreeCell> _subject;
        
        public void Select()
        {
            _selectionFrame.Show();
        }

        public void Deselect()
        {
            _selectionFrame.Hide();
        }

        public void UpdateData(Sprite abilityIcon, bool isUnlocked, int equippedSlot)
        {
            _abilityItem.SetData(abilityIcon, equippedSlot);

            if (equippedSlot != 0)
            {
                _equippedFrame.Show();
            }
            else
            {
                _equippedFrame.Hide();
            }
            
            _lockIcon.Show(!isUnlocked);

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
