using System;
using Code.Runtime.UI;
using Code.Runtime.UI.View;

namespace Code.Runtime.ConfigData.ScreenUI
{
    [Serializable]
    public class ScreenConfig
    { 
        public ScreenID ScreenID;
        public BaseWindowView WindowViewPrefab;
    }
}