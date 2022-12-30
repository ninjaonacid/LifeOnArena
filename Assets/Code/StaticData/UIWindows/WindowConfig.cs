using System;
using Code.UI;

namespace Code.StaticData.UIWindows
{
    [Serializable]
    public class WindowConfig
    {
        public UIWindowID WindowId;
        public WindowBase Prefab;
    }
}