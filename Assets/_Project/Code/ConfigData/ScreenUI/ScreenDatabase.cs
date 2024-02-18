using System.Collections.Generic;
using UnityEngine;

namespace Code.ConfigData.ScreenUI
{
    [CreateAssetMenu(menuName = "Config/Screen/ScreenDatabase", fileName = "ScreenDatabase")]
    public class ScreenDatabase : ScriptableObject
    {
        public List<ScreenConfig> Configs;
    }
}
