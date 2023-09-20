using System;
using Code.UI;
using Code.UI.View;

namespace Code.ConfigData.UIWindows
{
    [Serializable]
    public class ScreenConfig
    { 
        public ScreenID ScreenID;
        public BaseView ViewPrefab;
    }
}