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
        
        public async UniTaskVoid UpdateCooldown(AbilityTemplateBase ability)
        {
            CooldownImage.gameObject.SetActive(true);
            CooldownText.gameObject.SetActive(true);

            while (ability.CurrentCooldown >= 0)
            {
                CooldownText.SetText(Mathf.RoundToInt(ability.CurrentCooldown).ToString());
                CooldownImage.fillAmount = ability.CurrentCooldown / ability.Cooldown;
                await UniTask.Yield();
            }

            CooldownText.gameObject.SetActive(false);
            CooldownImage.gameObject.SetActive(false);
        }
    }
}