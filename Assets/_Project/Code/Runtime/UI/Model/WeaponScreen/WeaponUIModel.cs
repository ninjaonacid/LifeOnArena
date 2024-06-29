using UnityEngine;
using UnityEngine.Localization;

namespace Code.Runtime.UI.Model.WeaponScreen
{
    public class WeaponUIModel
    {
        public Sprite WeaponIcon;
        public LocalizedString WeaponName;
        public LocalizedString WeaponDescription;
        public int WeaponId;
        public bool isUnlocked;
        public bool isEquipped;
    }
}