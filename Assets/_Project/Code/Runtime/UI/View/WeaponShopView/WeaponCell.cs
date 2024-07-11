using System;
using Code.Runtime.ConfigData.Identifiers;
using TMPro;
using UniRx;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.WeaponShopView
{
    public class WeaponCell : MonoBehaviour, IPointerClickHandler
    {
        public WeaponId WeaponId;
        [SerializeField] private Image _weaponImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private CanvasElement _selectionFrame;
        [SerializeField] private CanvasElement _equippedFrame;
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

        public void UpdateView(Sprite weaponIcon, string weaponName, string weaponDescription, bool isUnlocked, bool isEquipped)
        {
            _weaponImage.sprite = weaponIcon;
            _weaponName.text = weaponName;
            _weaponDescription.text = weaponDescription;
            _lockImage.gameObject.SetActive(!isUnlocked);

            _equippedFrame.Show(isEquipped);

        }
        
        public void Select()
        {
            _selectionFrame.Show();
        }

        public void Deselect()
        {
            _selectionFrame.Hide();
        }
    }
}
