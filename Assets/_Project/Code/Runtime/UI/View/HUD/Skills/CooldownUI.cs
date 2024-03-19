using Code.Runtime.Modules.AbilitySystem;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.HUD.Skills
{
    public class CooldownUI : MonoBehaviour
    {
        public Image CooldownImage;
        public TextMeshProUGUI CooldownText;
        
        public async UniTaskVoid UpdateCooldown(ActiveAbility activeAbility)
        {
            CooldownImage.gameObject.SetActive(true);
            CooldownText.gameObject.SetActive(true);

            while (activeAbility.CurrentCooldown >= 0)
            {
                CooldownText.SetText(Mathf.RoundToInt(activeAbility.CurrentCooldown).ToString());
                CooldownImage.fillAmount = activeAbility.CurrentCooldown / activeAbility.Cooldown;
                await UniTask.Yield();
            }

            CooldownText.gameObject.SetActive(false);
            CooldownImage.gameObject.SetActive(false);
        }
    }
}