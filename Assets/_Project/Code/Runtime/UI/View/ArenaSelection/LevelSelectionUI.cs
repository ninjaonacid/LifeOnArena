using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.ArenaSelection
{
    public class LevelSelectionUI : MonoBehaviour
    {
        [SerializeField] private Image _levelImage;


        public void UpdateData(Sprite icon, bool isUnlocked)
        {
            _levelImage.sprite = icon;
        }
    }
}
