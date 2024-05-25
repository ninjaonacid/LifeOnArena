using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.UI.View.ArenaSelection
{
    public class LevelSelectionContainer : MonoBehaviour
    {
        [SerializeField] private List<LevelSelectionUI> _levelSelectionItems;

        public void UpdateData(int itemIndex, Sprite icon, bool isUnlocked)
        {
            _levelSelectionItems[itemIndex].UpdateData(icon, isUnlocked);
        }
        
    }
}
