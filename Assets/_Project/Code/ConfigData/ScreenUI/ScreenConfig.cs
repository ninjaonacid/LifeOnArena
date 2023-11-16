using System;
using Code.UI;
using Code.UI.View;

namespace Code.ConfigData.ScreenUI
{
    [Serializable]
    public class ScreenConfig
    { 
        public ScreenID ScreenID;
        public BaseView ViewPrefab;
    }
}