using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.View.ArenaSelection
{
    public class DifficultyInfo : MonoBehaviour
    {
        [SerializeField] private List<ImageUI> _difficultyIcons;
        [SerializeField] private TextMeshProUGUI _text;
        
        public void SetDifficultyInfo(int difficulty)
        {
            foreach (var icon in _difficultyIcons)
            {
                icon.Show(false);
            }
            
            for (int i = 0; i < difficulty; i++)
            {
                _difficultyIcons[i].Show(true);
            }
        }
    }
}
