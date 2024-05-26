using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.AbilityMenu
{
    public class AbilityItemView : MonoBehaviour
    {
        [SerializeField] private Image _abilityIcon;
        [SerializeField] private TextMeshProUGUI _equippedSlotIndex;
        

        private void Awake()
        {
            _equippedSlotIndex.color = new Color(1, 1, 0);
        }

        public void SetData(Sprite icon, int equippedSlotIndex = 0)
        {
            _abilityIcon.sprite = icon;

            if (equippedSlotIndex == 0)
            {
                _equippedSlotIndex.gameObject.SetActive(false);
            }
            else
            {
                _equippedSlotIndex.gameObject.SetActive(true);
                _equippedSlotIndex.text = equippedSlotIndex.ToString();
            }
            
        }

    }
}
