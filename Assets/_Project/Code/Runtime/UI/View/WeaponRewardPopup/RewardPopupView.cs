using TMPro;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.WeaponRewardPopup
{
    public class RewardPopupView : BaseWindowView
    {
        public TextMeshProUGUI RewardName;
        public Image RewardIcon;
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
