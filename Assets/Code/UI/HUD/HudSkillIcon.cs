using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.HUD
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
