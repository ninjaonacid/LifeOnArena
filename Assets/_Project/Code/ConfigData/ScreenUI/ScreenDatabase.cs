using System.Collections.Generic;
using UnityEngine;

namespace Code.ConfigData.UIWindows
{
    [CreateAssetMenu(menuName = "Config/Screen/ScreenDatabase", fileName = "ScreenDatabase")]
    public class ScreenDatabase : ScriptableObject
    {
        public List<ScreenConfig> Configs;
    }
}
