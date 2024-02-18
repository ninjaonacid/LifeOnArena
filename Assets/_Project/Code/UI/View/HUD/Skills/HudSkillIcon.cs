using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.View.HUD.Skills
{
    [RequireComponent(typeof(Image))]
    public class HudSkillIcon : MonoBehaviour
    {
        public Image Image;

        private void Awake()
        {
            Image = GetComponent<Image>();
        }
    }
}
