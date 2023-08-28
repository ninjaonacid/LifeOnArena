using System;
using Code.UI;
using Code.UI.View;
using UnityEngine.Serialization;

namespace Code.StaticData.UIWindows
{
    [Serializable]
    public class WindowConfig
    { 
        public ScreenID ScreenID;
        public ScreenBase Prefab;
        public IScreenView prefab;
    }
}