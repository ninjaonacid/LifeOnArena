using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.WeaponShopView
{
    public class WeaponCell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _weaponImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private CanvasGroup _selectionFrame;
        [SerializeField] private TextMeshProUGUI _weaponName;
        [SerializeField] private TextMeshProUGUI _weaponDescription;

        private Subject<WeaponCell> _weaponCellSubject;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _weaponCellSubject.OnNext(this);
        }

        public IObservable<WeaponCell> OnClickAsObservable()
        {
            return _weaponCellSubject ??= new Subject<WeaponCell>();
        }

        public void UpdateView(Sprite weaponIcon, string weaponName, string weaponDescription, bool isUnlocked)
        {
            _weaponImage.sprite = weaponIcon;
            _weaponName.text = weaponName;
            _weaponDescription.text = weaponDescription;
            _lockImage.gameObject.SetActive(!isUnlocked);
            
        }
        
        public void Select()
        {
            _selectionFrame.alpha = 1;
        }

        public void Deselect()
        {
            _selectionFrame.alpha = 0;
        }
    }
}
