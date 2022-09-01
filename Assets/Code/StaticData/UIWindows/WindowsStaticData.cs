using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Code.StaticData.UIWindows
{
    [CreateAssetMenu(menuName = "StaticData/WindowData", fileName = "WindowStaticData")]
    public class WindowsStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}
