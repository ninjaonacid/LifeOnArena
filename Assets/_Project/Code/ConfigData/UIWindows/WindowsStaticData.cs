using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData.UIWindows
{
    [CreateAssetMenu(menuName = "StaticData/WindowData", fileName = "WindowStaticData")]
    public class WindowsStaticData : ScriptableObject
    {
        public List<ScreenConfig> Configs;
    }
}
