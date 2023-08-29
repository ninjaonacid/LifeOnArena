using System;
using Code.UI;
using Code.UI.View;

namespace Code.StaticData.UIWindows
{
    [Serializable]
    public class ScreenConfig
    { 
        public ScreenID ScreenID;
        public BaseView ViewPrefab;
    }
}