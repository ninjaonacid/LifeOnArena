using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.SkillsMenu 
{
    [RequireComponent(typeof(Image))]
    public class SkillHolderIcon : MonoBehaviour
    {
        public Image Image => _image;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }
    }
}
