using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.ConfigData.ScreenUI
{
    [CreateAssetMenu(menuName = "Config/Screen/WindowDatabase", fileName = "WindowDatabase")]
    public class WindowDatabase : ScriptableObject
    {
        public List<ScreenConfig> Configs;
    }
}
