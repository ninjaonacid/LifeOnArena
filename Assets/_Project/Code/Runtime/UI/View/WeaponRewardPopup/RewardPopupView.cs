using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.WeaponRewardPopup
{
    public class RewardPopupView : BaseWindowView
    {
        public TextMeshProUGUI WeaponName;
        public Image WeaponIcon;
        public ClaimButton ClaimButton;
        
        public override void Show()
        {
            base.Show();
            
        }

        public override void Close()
        {
            base.Close();
        }
    }
}
